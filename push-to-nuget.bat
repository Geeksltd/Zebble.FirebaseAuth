#dotnet clean
#dotnet build --configuration Release
dotnet nuget push ".\bin\Release\*.nupkg" --source https://api.nuget.org/v3/index.json --api-key oy2h4qwjsox5nhvhxtnaesleoq7hbn5ujpk3ci4fj266za
