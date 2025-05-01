## Description

This application is a user feedback management system for the AccessShield desktop application, developed using the ASP.NET Core framework and C#.NET.This application is a user feedback management system for the AccessShield desktop application, developed using the ASP.NET Core framework and C#.NET.<br/>

## Required Steps
Before configuring and creating the database, it is necessary to download and install the PostgreSQL server, available online at the following link: [<b>https://www.postgresql.org/download/</b>](https://www.postgresql.org/download/). From the <b>pgAdmin 4</b> interface, create the 'messaging' database for the default user 'postgres' with the password 'test1234'. Then, using the migration instructions provided by the Entity Framework below, generate the required tables and their structure.<br/>
```powershell
PM> Update-Database
PM> Remove-Migration
```
<br/>
<b>If you prefer using the command line, enter the following instructions:</b>
<br/><br/>

```shell

> dotnet ef database update
> dotnet ef migrations remove

```
