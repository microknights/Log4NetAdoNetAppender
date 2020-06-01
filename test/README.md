# Testing with database

The `AdoNetAppender` is used to log to various databases, we setup test database tables for; sqlserver, postgresql, oracle, and mysql. Each database will have a database called `log4net` and several tables created.

The database and tables must be create manually,  a the user `log4net` with password `Abcd!234` will be used to write to the tables.


## Sqlserver
```
create table Log4NetNotNullable (
	[Date] datetime not null,
	[Thread] nvarchar(255) not null, 
	[Level]  nvarchar(50) not null, 
	[Logger] nvarchar(255) not null, 
	[Number] int not null, 
	[Message]   nvarchar(4000) not null, 
	[Exception] nvarchar(4000) not null)
GO
create table Log4NetNullable (
	[Date] datetime null,
	[Thread] nvarchar(255) null, 
	[Level]  nvarchar(50) null, 
	[Logger] nvarchar(255) null, 
	[Number] int null, 
	[Message]   nvarchar(4000) null, 
	[Exception] nvarchar(4000) null)
GO
```