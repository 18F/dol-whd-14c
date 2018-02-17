# Department of Labor - Wage and Hour - Section 14c

##### Web
 [![CircleCI](https://circleci.com/gh/18F/dol-whd-14c.svg?style=svg)](https://circleci.com/gh/18F/dol-whd-14c)
 [![codecov](https://codecov.io/gh/18f/dol-whd-14c/branch/master/graph/badge.svg)](https://codecov.io/gh/18f/dol-whd-14c)
 [![Maintainability](https://api.codeclimate.com/v1/badges/14bad4b687fc87cb2d13/maintainability)](https://codeclimate.com/github/18F/dol-whd-14c/maintainability)
[![Dependenc Status](https://gemnasium.com/badges/github.com/18F/dol-whd-14c.svg)](https://gemnasium.com/github.com/18F/dol-whd-14c)

##### API
[build status missing] [coverage missing] [quality missing] [dependency status missing]

## Table of contents

 - [Project History](#history)
 - [Users](#users)
 - [Problem being solved and project goals](#problem-being-solved-and-project-goals)
 - [Project Management](#project-management)
 - [Technology Stack](#technology-stack)
 - [System Context Diagram](#system-context)
 - [Conceptual Physical Architecture](#conceptual-physical-architecture)
 - [Application Components](#application-components)
    - [User Interface](#user-interface)
    - [REST Services](#rest-services)
    - [Database](#database)
 - [Deploying](#deploying)
    - [Conceptual Deployment Model](#conceptual-deployment-model)
    - [WebDeploy Packages / Build Artifacts](#webdeploy-packages-and-build-artifacts)
     1. [DOL.WHD.Section14c.Web.zip](#1-dolwhdsection14cwebzip)
       * [Configuration Settings](#configuration-settings)
     2. [DOL.WHD.Section14c.API.zip](#2-dolwhdsection14capizip)
       * [Configuration Settings](#configuration-settings-1)
     3. [DotNet.CoverageReport.zip](#3-dotnetcoveragereportzip)

## History

The 14(c) system is a form-based process currently relying on paper submissions that will become a modern, digital-first service to assist with a Department of Labor Wage and Hour Division (WHD) program to help employ workers with disabilities. Applicants will be provided an intuitive online experience, guiding them through the information needed to complete their application correctly.

This work began several years ago during a workshop WHD and 18F ran to articulate 14(c) mission, users, and needs, including sessions on prototyping and risk assessment.

To deliver this work, WHD and 18F have conducted two engagements using the Agile Delivery Services BPA (Agile BPA) to help WHD hire a vendor so they can build an online application together that can gain an ATO and be deployed into a production environment and, in the future, maintained by WHD without 18F or even vendor support. Through this, 18F helped build capacity at WHD and in the vendor community way by modeling and coaching in modern software development practices so agencies and vendors can develop products in an agile, human-centered, outcome-oriented way.

More information about this engagement is in the [Agile BPA Task Order](https://github.com/18F/bpa-DOL-WHD-14-c).

More information about the 14(c) program can be found in this [fact sheet](https://www.dol.gov/whd/regs/compliance/whdfs39.htm).

## Users

End users of the application are:
 - Employers who submit 14(c) certificate applications through the online interface
 - DOL WHD personnel who need to track, review, and update submitted applications as they move through the review and approval process
 - Administrators who will control authorization to view the 14(c) applications

## Problem being solved and project goals

Section 14(c) certification is currently a paper-based process. Applicants download PDFs of the paper application from DOL's website, complete the forms by hand, and submit them to WHD via regular mail. Recognizing that this process is slow and cumbersome for both applicants and the WHD staff responsible for reviewing and issuing section 14(c) certificates, WHD began collaborating with 18F to modernize the process.

## Project Management

Install [Zenhub](https://www.zenhub.com/) extension and view [project board](https://github.com/18F/dol-whd-14c#boards).

### Technology Stack

![Technology Stack](docs/TechStack.png?raw=true "Technology Stack")

### System Context

![System Context](docs/SystemContext.png?raw=true "System Context")

### Conceptual Physical Architecture

![Conceptual Physical Architecture](docs/ConceptualPhysicalArchitecture.png?raw=true "Conceptual Physical Architecture")

## Application Components

### User Interface

See [DOL.WHD.Section14c.Web Readme](DOL.WHD.Section14c.Web/Readme.md)

| Project | Description     
| --- | ---
DOL.WHD.Section14c.Web | AngularJS front-end

### REST Services
```
DOL.WHD.Section14c.sln
```
| Project | Description     
| --- | ---
DOL.WHD.Section14c.Api | ASP.Net WebAPI REST Services
DOL.WHD.Section14c.Business | .Net Class Library for business services
DOL.WHD.Section14c.Business.Test | MSTest Unit Tests
DOL.WHD.Section14c.Common | .Net Class Libraray for shared coded
DOL.WHD.Section14c.Domain | .Net Class Libarary for entities
DOL.WHD.Section14c.DataAccess | .Net Class Libarary for Entity Framework Context

### Database

The PostgreSQL database used to persist user and 14c application data is generated by Entity Framework Code First Data Migration using the DB2 provider.  The migration includes all required seed data to run the application.

[Schema Diagram](docs/dol.whd.section14c.dbschema.png)

[Migration Configuration](DOL.WHD.Section14c.DataAccess/Migrations/Configuration.cs)

#### System Administrator User Seed

To estblish an intial System Administrator user account the database migration seeds an account for `14c-admin@dol.gov`.  By default this accounts password is expired and must be reset at the first login or through the forgot password flow.

## Deploying

### Conceptual Deployment Model

![Conceptual Deployment Model](docs/ConceptualDeploymentModel.png?raw=true "Conceptual Deployment Model")

### WebDeploy Packages and Build Artifacts

The projects AppVeyor builds generates three web deployment artifacts.  They can be found in the build under the artifacts tab.  As noted in the conceptual deployment model, they can be deployed manually through a [IIS Application Import](https://www.iis.net/learn/publish/using-web-deploy/import-a-package-through-iis-manager) or through the [MSDdeploy command](https://technet.microsoft.com/en-us/library/dd569106(v=ws.10).aspx).  For continuous integration settings through AppVeyor see the [appveyor.yml](appveyor.yml)

### 1. DOL.WHD.Section14c.Web.zip
This package contains the front end Web Application.  This is a zip of the WebPack production configuration output found in the /dist folder.  It is not a native WebDeploy package but can be used as one as configured in the AppVeyor Continuous Deployment.

#### Configuration Settings
All configurations for the Web project are set in the env.js. This file is excluded from Continuous Deployment and should be updated manually if needed.

| Setting | Description | Default   
| --- | --- | ---
api_url* | Full URL of the REST API Service | http://localhost:3334 (Local develoment URL)
requireHttps | Use secure cookie | true
tokenCookieDurationMinutes | Token Cookie Expiration in Minutes | 20160 (14 days to match server AccessTokenExpireTimeSpanMinutes)

\* Must be configured during deployment

### 2. DOL.WHD.Section14c.API.zip

This package is a Web the REST Api
#### Configuration Settings
The following are setup as WebDeploy parameters and can be set with command line MSDeploy arguments or manually via a IIS MMC WebDeploy Package Import.  The parameters are setup to transform their respective values in the Web.config file.  An alternate deployment option would be to exclude the Web.config file and set them in an alternate configuration management process.

| Setting | Description | Default   
| --- | --- | ---
ConnectionString* | PostgreSQL database connection string | No Deployment Default
SMTPServer* | SMTP Email Server Address | localhost
SMTPPort* | SMTP Email Server Port | 25
SMTPUserName* | SMTP Email Server User Name | empty
SMTPPassword* | SMTP Email Server | empty
EmailFrom* | SMTP Email Server from address | no-reply@dol.gov
AttachmentRepositoryRootFolder* | File Path (Local or UNC path) where application attachments should be stored | No Deployment Default
UserLockoutEnabledByDefault | Enables or Disabled user login attempt lockout | true
DefaultAccountLockoutTimeSpan | Minutes to lockout user | 15
MaxFailedAccessAttemptsBeforeLockout | Login attempts befer user is locked out | 3
PasswordExpirationDays | Number of days before password must be changes | 90
AccessTokenExpireTimeSpanMinutes | Token Expiration Minutes for Reset Password and Email Verification Links | 20160 (14 days, ASP.net Default)
AllowedFileNamesRegex | Regex for allowed filenames | ^(.*\.(doc|docx|xls|xlsx|ppt|pptx|pdf)$)?[^.]*$
RequireHttps | Require HTTPS for secure communication | true

\* Must be configured during deployment


### 3. DotNet.CoverageReport.zip

This artifact contains static files of a ReportGenerator code coverage report for .Net.  This is a development artifact and is not needed for production.

## Public domain

This project is in the worldwide [public domain](LICENSE.md).   As stated in [CONTRIBUTING](CONTRIBUTING.md):

> This project is in the public domain within   the United States, and copyright and related rights in the
> work worldwide are waived through the
> [CC0 1.0 Universal public domain dedication](https://creativecommons.org/publicdomain/zero/1.0/).  
>
> All contributions to this project will be released under the CC0 dedication. By submitting a pull request,
> you are agreeing to comply with this waiver of copyright interest.
