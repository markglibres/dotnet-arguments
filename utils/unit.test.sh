#!/bin/bash
set -e

FILEPATH=$(dirname "$0")
SCRIPTDIR=$(cd "$(dirname "$FILEPATH")"; pwd -P)/$(basename "$FILEPATH")

cd $SCRIPTDIR
cd ../src

CONFIGURATION="Release"
echo "projects to run: ${PROJ}"

for proj in $(find . -name "*UnitTest*.csproj")
do
    echo "running project: ${proj}"
    dotnet restore -v q $proj
    dotnet build -v q $proj --configuration $CONFIGURATION
    dotnet test $proj --configuration $CONFIGURATION -l "console;verbosity=detailed" -v n --no-build
done
