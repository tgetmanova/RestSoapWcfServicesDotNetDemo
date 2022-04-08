@echo off

if "%ProgramFiles(x86)%"=="" GOTO x86
set FRAMEWORK_WSDL_GENERATOR="%ProgramFiles(x86)%\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\svcutil.exe"
goto frameworkset
:x86
set FRAMEWORK_WSDL_GENERATOR="%ProgramFiles%\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\svcutil.exe"
:frameworkset

set WORKING_FOLDER="."
set LIBRARY_NAME=UserRepositoryServiceProxy

set USER_REPOSITORY_SERVICE=http://localhost:2828/UserInfoProviderService?wsdl

set TARGET_SERVICE=%USER_REPOSITORY_SERVICE%

echo *** Delete old file [%WORKING_FOLDER%\%LIBRARY_NAME%.cs]
del %WORKING_FOLDER%\%LIBRARY_NAME%.cs
del %WORKING_FOLDER%\%LIBRARY_NAME%.config

echo *** Generate new source code [%LIBRARY_NAME%.cs]
%FRAMEWORK_WSDL_GENERATOR% /serializer:DataContractSerializer /out:%LIBRARY_NAME% /config:%LIBRARY_NAME%.config /namespace:*,UserRepositoryServiceTests.Proxy /synconly /nologo %TARGET_SERVICE%
echo *** All done