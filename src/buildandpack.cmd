@echo off
set version=2.0.0-alpha



dotnet clean MicroKnights.Log4NetAdoNetAppender.csproj
dotnet build -c release MicroKnights.Log4NetAdoNetAppender.csproj
dotnet pack -c release -o r:\nuget --version-suffix %version% MicroKnights.Log4NetAdoNetAppender.csproj

dotnet clean MicroKnights.Log4NetAdoNetAppender.StrongName.csproj
dotnet build -c release MicroKnights.Log4NetAdoNetAppender.StrongName.csproj
dotnet pack -c release -o r:\nuget --version-suffix %version% MicroKnights.Log4NetAdoNetAppender.StrongName.csproj

