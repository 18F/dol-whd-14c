﻿
Pushd "%~dp0"

nuget install TestRunner -Version 1.2.0 -OutputDirectory tools
nuget install OpenCover -Version 4.6.519 -OutputDirectory tools
nuget install ReportGenerator -Version 2.4.5 -OutputDirectory tools

.\tools\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:.\tools\TestRunner.1.2.0\tools\TestRunner.exe -targetargs:".\bin\Debug\DOL.WHD.Section14c.Test.dll" -filter:"+[*]DOL.WHD.Section14c.* -[*.Tests]* -[*.Test]* -[*]DOL.WHD.Section14c.DataAccess.* -[*]DOL.WHD.Section14c.Log.Migrations.* -[*]DOL.WHD.Section14c.Log.DataAccess.* -[*]DOL.WHD.Section14c.Log.Areas.* -[*]DOL.WHD.Section14c.Log.Properties* -[*]DOL.WHD.Section14c.Log.WebApiApplication* -[*]DOL.WHD.Section14c.Log.DependencyResolutionConfig* -[*]DOL.WHD.Section14c.Log.RouteConfig* -[*]DOL.WHD.Section14c.Log.FilterConfig* -[*]DOL.WHD.Section14c.Log.WebApiConfig* -[*]DOL.WHD.Section14c.PdfApi.Areas.* -[*]DOL.WHD.Section14c.PdfApi.WebApiApplication* -[*]DOL.WHD.Section14c.PdfApi.App_Start.DependencyResolutionConfig* -[*]DOL.WHD.Section14c.PdfApi.RouteConfig* -[*]DOL.WHD.Section14c.PdfApi.FilterConfig* -[*]DOL.WHD.Section14c.PdfApi.WebApiConfig* -[*]DOL.WHD.Section14c.EmailApi.Areas.* -[*]DOL.WHD.Section14c.EmailApi.WebApiApplication* -[*]DOL.WHD.Section14c.EmailApi.App_Start.DependencyResolutionConfig* -[*]DOL.WHD.Section14c.EmailApi.RouteConfig* -[*]DOL.WHD.Section14c.EmailApi.FilterConfig* -[*]DOL.WHD.Section14c.EmailApi.WebApiConfig*" -register:user

.\tools\ReportGenerator.2.4.5.0\tools\ReportGenerator.exe -reports:results.xml -targetdir:CoverageReport -reporttypes:Badges;Html -historydir:CoverageReport\History
