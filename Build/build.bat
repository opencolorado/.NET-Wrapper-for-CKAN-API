REM Read the version from the version.txt file
set /p version= <version.txt
echo Building outputs for version %version%

REM Build the web project in release mode
%systemroot%\microsoft.net\framework\v4.0.30319\msbuild.exe ..\CkanDotNet.Web\CkanDotNet.Web.csproj /T:Package /P:Configuration=Release

REM Create the release package for CkanDotNet.Web
del Output\CkanDotNet.Web.%version%.zip
Tools\7zip\7za.exe a -r -tzip Output\CkanDotNet.Web.%version%.zip ..\CkanDotNet.Web\obj\Release\Package\PackageTmp\*

REM Build the API project in release mode
%systemroot%\microsoft.net\framework\v4.0.30319\msbuild.exe ..\CkanDotNet.Api\CkanDotNet.Api.csproj /P:Configuration=Release /p:DebugSymbols=false /p:DebugType=None

REM Create the release package for CkanDotNet.Api
del Output\CkanDotNet.Api.%version%.zip
Tools\7zip\7za.exe a -r -tzip Output\CkanDotNet.Api.%version%.zip ..\CkanDotNet.Api\bin\Release\*

