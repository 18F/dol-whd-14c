
Pushd "%~dp0"

C:\nuget.exe install TestRunner -Version 1.2.0 -OutputDirectory tools
C:\nuget.exe install OpenCover -Version 4.6.519 -OutputDirectory tools
C:\nuget.exe install ReportGenerator -Version 2.4.5 -OutputDirectory tools

.\tools\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:.\tools\TestRunner.1.2.0\tools\TestRunner.exe -targetargs:".\bin\Debug\DOL.WHD.Section14c.Test.dll" -filter:"+[*]DOL.WHD.Section14c.* -[*.Tests]* -[*.Test]* -[*]DOL.WHD.Section14c.DataAccess.*" -register:user

.\tools\ReportGenerator.2.4.5.0\tools\ReportGenerator.exe -reports:results.xml -targetdir:CoverageReport -reporttypes:Badges;Html -historydir:CoverageReport\History
