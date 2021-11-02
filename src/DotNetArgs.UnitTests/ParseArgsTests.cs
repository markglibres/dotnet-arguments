using System.Linq;
using DotNetArgs.UnitTests.Abstract;
using FluentAssertions;
using Xunit;

namespace DotNetArgs.UnitTests
{
    public class ParseArgsTests : TestSpecification<string[], string[]>
    {
        [Fact]
        public void Should_Parse_Array_Items()
        {
            Given(() => new[]
            {
                "--indexes",
                "[1,2,3]"
            });

            When(ConfigurationExtensions.ParseArgs);

            Then((_, result) =>
            {
                var args = result.ToList();
                args.Should().NotBeNull();
                args.Count.Should().Be(6);

                for (var i = 0; i < 3; i++)
                {
                    var key = $"--indexes:{i}";
                    var index = result
                        .ToList()
                        .IndexOf(key);

                    args.Should().Contain(arg => arg == key);
                    args[index + 1].Should().Be((i + 1).ToString());
                }
            });
        }

        [Fact]
        public void Should_Not_Parse_Non_Array_Items()
        {
            Given(() => new[]
            {
                "--indexes",
                "[1,2,3]",
                "--notIndexes",
                "1,2,3"
            });

            When(ConfigurationExtensions.ParseArgs);

            Then((_, result) =>
            {
                var args = result.ToList();
                args.Should().NotBeNull();
                args.Count.Should().Be(8);

                args.Should().Contain("--notIndexes");
                args.Should().Contain("1,2,3");
            });
        }
    }
}
