#!/bin/bash
cd src/

case "$TRAVIS_BRANCH" in
  "master")
    dotnet pack /p:PackageVersion=1.0.$TRAVIS_BUILD_NUMBER --no-restore -o .
    dotnet nuget push *.nupkg -k $NUGET_API_KEY -s https://api.nuget.org/v3/index.json
    ;;
  "develop")
    dotnet pack /p:PackageVersion=1.0.$TRAVIS_BUILD_NUMBER-dev --no-restore -o .
    dotnet nuget push *.nupkg -k $NUGET_API_KEY -s https://api.nuget.org/v3/index.json
    ;;    
esac