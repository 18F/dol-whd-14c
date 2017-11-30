#!/usr/bin/env bash

nuget install TestRunner -Version 1.2.0 -OutputDirectory tools
nuget install OpenCover -Version 4.6.519 -OutputDirectory tools
nuget install ReportGenerator -Version 2.4.5 -OutputDirectory tools

export MONO_IOMAP=all

mono --debug --profile=log:coverage,onlycoverage\
,covfilter=-Castle\
,covfilter=-FluentValidation\
,covfilter=-Microsoft\
,covfilter=-Moq\
,covfilter=-TestRunner\
,covfilter=-DOL.WHD.Section14c.Log.Migrations\
,covfilter=-DOL.WHD.Section14c.Log.DataAccess\
,covfilter=-DOL.WHD.Section14c.Log.Areas\
,covfilter=-DOL.WHD.Section14c.Log.Properties\
,covfilter=-DOL.WHD.Section14c.Log.WebApiApplication\
,covfilter=-DOL.WHD.Section14c.Log.DependencyResolutionConfig\
,covfilter=-DOL.WHD.Section14c.Log.RouteConfig\
,covfilter=-DOL.WHD.Section14c.Log.FilterConfig\
,covfilter=-DOL.WHD.Section14c.Log.WebApiConfig\
,covfilter=-DOL.WHD.Section14c.PdfApi.Areas\
,covfilter=-DOL.WHD.Section14c.PdfApi.WebApiApplication\
,covfilter=-DOL.WHD.Section14c.PdfApi.App_Start.DependencyResolutionConfig\
,covfilter=-DOL.WHD.Section14c.PdfApi.RouteConfig\
,covfilter=-DOL.WHD.Section14c.PdfApi.FilterConfig\
,covfilter=-DOL.WHD.Section14c.PdfApi.WebApiConfig\
,covfilter=-DOL.WHD.Section14c.EmailApi.Areas\
,covfilter=-DOL.WHD.Section14c.EmailApi.WebApiApplication\
,covfilter=-DOL.WHD.Section14c.EmailApi.App_Start.DependencyResolutionConfig\
,covfilter=-DOL.WHD.Section14c.EmailApi.RouteConfig\
,covfilter=-DOL.WHD.Section14c.EmailApi.FilterConfig\
,covfilter=-DOL.WHD.Section14c.EmailApi.WebApiConfig\
,covfilter=-DOL.WHD.Section14c.Test\
  ./tools/TestRunner.1.2.0/tools/TestRunner.exe \
  ./bin/Debug/DOL.WHD.Section14c.Test.dll

mprof-report --coverage-out=coverage.xml output.mlpd > /dev/null
