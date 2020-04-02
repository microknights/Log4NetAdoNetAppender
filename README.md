# Log4NetAdoNetAppender
.Net Standard 2.0 AdoNetAppender

A late night fix since it do not exists for .net core for some odd reason. This project will only exists until the official [logging.apache.org](https://logging.apache.org/) project is releasing their own AdoNetAppender within the log4net package.

## How to setup

You log4net.config AdoNetAppender configuration (Read about use of Microsoft.Data.SqlClient below!)

```
<log4net>
  <appender name="AdoNetAppender" type="MicroKnights.Logging.AdoNetAppender, MicroKnights.Log4NetAdoNetAppender">
    <bufferSize value="1" />
    <connectionType value="System.Data.SqlClient.SqlConnection,System.Data.SqlClient,Version=4.0.0.0,Culture=neutral,PublicKeyToken=b77a5c561934e089" />
    <connectionStringName value="log4net" />
    <connectionStringFile value="connectionstrings.json" />
    <commandText value="INSERT INTO Log ([Date],[Thread],[Level],[Logger],[Message],[Exception]) VALUES (@log_date, @thread, @log_level, @logger, @message, @exception)" />
    <parameter>
        <parameterName value="@log_date" />
        <dbType value="DateTime" />
        <layout type="log4net.Layout.RawTimeStampLayout" />
    </parameter>
    <parameter>
        <parameterName value="@thread" />
        <dbType value="String" />
        <size value="255" />
        <layout type="log4net.Layout.PatternLayout">
            <conversionPattern value="%thread" />
        </layout>
    </parameter>
    <parameter>
        <parameterName value="@log_level" />
        <dbType value="String" />
        <size value="50" />
        <layout type="log4net.Layout.PatternLayout">
            <conversionPattern value="%level" />
        </layout>
    </parameter>
    <parameter>
        <parameterName value="@logger" />
        <dbType value="String" />
        <size value="255" />
        <layout type="log4net.Layout.PatternLayout">
            <conversionPattern value="%logger" />
        </layout>
    </parameter>
    <parameter>
        <parameterName value="@message" />
        <dbType value="String" />
        <size value="4000" />
        <layout type="log4net.Layout.PatternLayout">
            <conversionPattern value="%message" />
        </layout>
    </parameter>
    <parameter>
        <parameterName value="@exception" />
        <dbType value="String" />
        <size value="2000" />
        <layout type="log4net.Layout.ExceptionLayout" />
    </parameter>
  </appender>
```

With 2 changes, you are up and running in your .net core project.

First you must change/replace the type and assembly name, from this:
```
<appender name="AdoNetAppender" type="log4net.Appender.AdoNetAppender">
```
to this:
```
<appender name="AdoNetAppender" type="MicroKnights.Logging.AdoNetAppender, MicroKnights.Log4NetAdoNetAppender">
```

Secondly you must specify the configurationfile where your connectionstrings are defined. A new property in the configuration is added - called `ConnectionStringFile`. It will then locate the connectionstring by using `ConnectionStringName`.

## Set connection at runtime
Instead of getting the connectionstring through a file, you can set it directly at runtime.

Take a peek at the [Log4NetHelper](https://github.com/microknights/Log4NetHelper) project, from where this is possible.
# NuGet Package
```
PM> Install-Package MicroKnights.Log4NetAdoNetAppender
```
## Note for .Net Core 3 (up til .Net standard 2.0)
Due to a fix in the Type loader, the configuration has a small change in the `connectionType` property.

Was previously:
`<connectionType value="System.Data.SqlClient.SqlConnection,System.Data,Version=4.0.0.0,Culture=neutral,PublicKeyToken=b77a5c561934e089" />`

and is now:

`<connectionType value="System.Data.SqlClient.SqlConnection,System.Data.SqlClient,Version=4.0.0.0,Culture=neutral,PublicKeyToken=b77a5c561934e089" />`

_this change also works backwards, for versions before .Net standard 2.0_

## Note for .Net Core 3.1+ (from .Net standard 2.1)
Microsoft has changed path for the SqlClient library, and we are following. Previously it was `System.Data.SqlClient` and now it is `Microsoft.Data.SqlClient`.

So we must reflect this change, and this is done on the `connectionType` property.

Was previously:
`<connectionType value="System.Data.SqlClient.SqlConnection,System.Data.SqlClient,Version=4.0.0.0,Culture=neutral,PublicKeyToken=b77a5c561934e089" />`

and is now:

`<connectionType value="Microsoft.Data.SqlClient.SqlConnection, Microsoft.Data.SqlClient, Version=1.0.0.0,Culture=neutral,PublicKeyToken=23ec7fc2d6eaa4a5"/>`

_this change do not work backwards, only for version .Net Standard 2.1 and forward_
