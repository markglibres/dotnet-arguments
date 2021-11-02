#!/bin/bash
set -e

NUGET_API_KEY=
while test $# -gt 0
do
    case "$1"
    in
        --nuget-api-key)
            shift
            if test $# -gt 0
            then
                NUGET_API_KEY=$1
            fi
            shift
            ;;
        *)
            shift
            break;;
    esac
done

if [[ -z $NUGET_API_KEY ]];
then
    echo "Error: NuGet Api Key is required. Use --nuget-api-key option"
    exit 0;
fi

FILEPATH=$(dirname "$0")
SCRIPTDIR=$(cd "$(dirname "$FILEPATH")"; pwd -P)/$(basename "$FILEPATH")

cd $SCRIPTDIR
cd ../src

dotnet pack
dotnet nuget push bin/Debug/DotNetArguments.*.nupkg --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json
