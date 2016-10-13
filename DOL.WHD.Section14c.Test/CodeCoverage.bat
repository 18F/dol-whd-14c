
Pushd "%~dp0"

nuget install TestRunner -Version 1.2.0 -OutputDirectory tools
nuget install OpenCover -Version 4.6.519 -OutputDirectory tools
nuget install ReportGenerator -Version 2.4.5 -OutputDirectory tools
 
.\tools\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:.\tools\TestRunner.1.2.0\tools\TestRunner.exe -targetargs:".\bin\Debug\DOL.WHD.Section14c.Test.dll" -filter:"+[*]* -[*.Tests]* -[*.Test]* -[*]DOL.WHD.Section14c.DataAccess.*" -register:user

.\tools\ReportGenerator.2.4.5.0\tools\ReportGenerator.exe -reports:results.xml -targetdir:CoverageReport -reporttypes:Badges;Html -historydir:CoverageReport\History