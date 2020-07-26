# Testing with database

The `AdoNetAppender` is used to log to various databases, we setup test database tables for; sqlserver, postgresql, oracle, and mysql. Each database will have a database called `log4net` and several tables created.

The database and tables must be create manually,  a the user `log4net` with password `Abcd!234` will be used to write to the tables.


## Sqlserver

### Tables
```
create table Log4NetNotNullable (
	[Date] datetime not null,
	[Thread] nvarchar(255) not null, 
	[Level]  nvarchar(50) not null, 
	[Logger] nvarchar(255) not null, 
	[Number] nvarchar(20) not null, 
	[Message]   nvarchar(4000) not null, 
	[Exception] nvarchar(4000) not null)
GO
create table Log4NetNullable (
	[Date] datetime null,
	[Thread] nvarchar(255) null, 
	[Level]  nvarchar(50) null, 
	[Logger] nvarchar(255) null, 
	[Number] nvarchar(20) null, 
	[Message]   nvarchar(4000) null, 
	[Exception] nvarchar(4000) null)
GO
create table Log4NetBuffering25 (
	[Date] datetime not null,
	[Thread] nvarchar(255) not null, 
	[Level]  nvarchar(50) not null, 
	[Logger] nvarchar(255) not null, 
	[Number] nvarchar(20) not null, 
	[Message]   nvarchar(4000) not null, 
	[Exception] nvarchar(4000) not null)
GO
```

## PostgreSql

### Tables
```
create table Log4NetNotNullable (
	Date timestamp not null,
	Thread varchar(255) not null,
	Level  varchar(50) not null,
	Logger varchar(255) not null,
	Number varchar(20) not null,
	Message   varchar(4000) not null,
	Exception varchar(4000) not null);

create table Log4NetNullable (
	Date timestamp null,
	Thread varchar(255) null,
	Level  varchar(50) null,
	Logger varchar(255) null,
	Number varchar(20) null,
	Message   varchar(4000) null,
	Exception varchar(4000) null);
```

## MySql
create table Log4NetNotNullable (
	Date timestamp not null,
	Thread varchar(255) not null,
	Level  varchar(50) not null,
	Logger varchar(255) not null,
	Number varchar(20) not null,
	Message   varchar(4000) not null,
	Exception varchar(4000) not null);

create table Log4NetNullable (
	Date timestamp null,
	Thread varchar(255) null,
	Level  varchar(50) null,
	Logger varchar(255) null,
	Number varchar(20) null,
	Message   varchar(4000) null,
	Exception varchar(4000) null);
```

