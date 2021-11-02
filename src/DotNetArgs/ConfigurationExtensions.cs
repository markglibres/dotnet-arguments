using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace DotNetArgs
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddCommandLineWithArrays(this IConfigurationBuilder builder, string[] args)
        {
            var newArgs = ParseArgs(args);
            return builder.AddCommandLine(newArgs);
        }

        public static string[] ParseArgs(string[] args)
        {
            var delimiter = "~";
            var regex = new Regex($@"(--?)([^{delimiter}]+){delimiter}\[([^{delimiter}]+)\]");
            var matches = regex
                .Matches(string.Join(delimiter, args))
                .ToList();

            var newArgs = regex
                .Replace(string.Join(delimiter, args), string.Empty)
                .Split(delimiter)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();

            matches.ForEach(match =>
            {
                var prefix = match.Groups[1].Value;
                var key = match.Groups[2].Value;
                var values = match.Groups[3].Value
                    .Split(",")
                    .Select((val, index) => (val, index))
                    .ToList();

                values.ForEach(tuple =>
                {
                    newArgs.Add($"{prefix}{key}:{tuple.index}");
                    newArgs.Add(tuple.val);
                });
            });
            return newArgs.ToArray();
        }
    }
}
