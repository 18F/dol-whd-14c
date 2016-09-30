
Pushd "%~dp0"

nuget install TestRunner -Version 1.2.0 -OutputDirectory tools
nuget install OpenCover -Version 4.6.519 -OutputDirectory tools
nuget install coveralls.net -Version 0.412.0 -OutputDirectory tools
 
.\tools\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:.\tools\TestRunner.1.2.0\tools\TestRunner.exe -targetargs:".\bin\Debug\DOL.WHD.Section14c.Test.dll" -filter:"+[*]* -[*.Tests]* -[*]DOL.WHD.Section14c.DataAccess.*" -register:user
 
.\tools\coveralls.net.0.412\tools\csmacnz.Coveralls.exe --opencover -i .\results.xml 