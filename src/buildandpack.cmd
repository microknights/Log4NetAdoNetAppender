@echo off
set version=2.2.1-alpha.2
set projectname=MicroKnights.Log4NetAdoNetAppender.csproj
set projectstrongname=MicroKnights.Log4NetAdoNetAppender.StrongName.csproj


dotnet clean -c Release %projectname%
dotnet build -c Release %projectname%
dotnet pack -c Release --no-build -o r:\nuget --version-suffix %version% %projectname%

dotnet clean -c Release %projectstrongname%
dotnet build -c Release %projectstrongname%
dotnet pack -c Release --no-build -o r:\nuget --version-suffix %version% %projectstrongname%

