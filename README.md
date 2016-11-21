# Image labeling platform
An image labeling platform to assess human observer labels or to perform a blinded reading. 
The platform is built up as a client-server architecture with a lightweight client (HTML web page frontend) and processing and
encrypted storing on server side.
Users can register to the platform and receive automatically an invitation to participate in a labeling when a new labeling 
test-case is generated. The users login to the platform via a web browser.

## Test-case
consists of:
- associated images
- specific test-case/labeling question
- test-case description
- 1 to M discrete or continuous labels
- [optional] guidance page which users can inspect anytime
- [optional] global best and worst control image

and the displayed web page front end contains:

(a) one displayed image, i.e. independent labeling

(b) 1 to N images in parallel, i.e. in relation to each other

(c) a reference image and 1 to N images in parallel

## User types
- admin: generate and manage test-cases, manage users, analyze and export labels, maintain image database
- user: access labeling test-cases

## Images
- images can be in DICOM, MHD or GIPL format
- viewing is realized via SimpleITK: https://github.com/SimpleITK/SimpleITK

--------------------------------------------------------
## Getting Started
1.) Set/modify the parameters (Email, database structure, ...) in
```
Utility/Constant.cs
```
specify your MySQL connection (SERVERNAME, SQL_ADMIN, SQL_ADMIN_PWD) in
```
Web.config (line 9): <add name="MySqlConnection" connectionString="Server=SERVERNAME;Database=labelingframework;Uid=SQL_ADMIN;Pwd=SQL_ADMIN_PWD;" />
```
[optional] modify CSS style sheet in
```
Styles/Site.css
```
2.) Compile the labeling framework library

Windows: Install Microsoft Visual Studio, open LabelingFramework.csproj and compile

Unix/OSX: Install Mono (http://www.mono-project.com/) and compile via
```
xbuild /p:Configuration=Release LabelingFramework.csproj
```
3.) Deploy to your web server

Windows/Unix/OSX: Apache via mod_mono (http://www.mono-project.com/docs/web/mod_mono/)

Windows: Internet Information Services (IIS; https://technet.microsoft.com/en-us/library/ee692294(v=ws.10).aspx)

4.) Import MySQL procedures and tables from
```
MySQL/default_database.sql
```
with default administrator (needed for successfull initial login, please change password after first login)

user: admin

password: 123456

--------------------------------------------------------
Please read LICENSE file for licensing details.

Detailed information are available at:

https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform
