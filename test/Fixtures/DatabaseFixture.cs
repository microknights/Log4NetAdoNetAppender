using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;
using log4net.Config;
using log4net.Core;
using MicroKnights.Logging;
using Microsoft.Extensions.Configuration;

namespace MicroKnights.Log4NetAdoNetAppender.Test.Fixtures
{
    public abstract class DatabaseFixture : IErrorHandler
    {
        public IConfiguration Configuration { get; private set; }

        protected abstract string DatabaseProductName { get; }
        public string ConnectionString { get; private set; }

        private readonly List<string> _log4netErrors = new List<string>();

        public void Error(string message, Exception e, ErrorCode errorCode)
        {
            _log4netErrors.Add($"{message}[{errorCode}]: {e.Message}");
        }

        public void Error(string message, Exception e)
        {
            _log4netErrors.Add($"{message}: {e.Message}");
        }

        public void Error(string message)
        {
            _log4netErrors.Add(message);
        }


        public IEnumerable<string> InitializeLog4Net( string tableAndConfigurationName)
        {
//            LogManager.ResetConfiguration(GetType().Assembly);
            var repository = LogManager.GetRepository(GetType().Assembly);

            Configuration = new ConfigurationBuilder()
                .AddJsonFile("connectionstrings.json")
                .Build();

            var databaseProductName = DatabaseProductName.ToLowerInvariant();
            ConnectionString = Configuration.GetConnectionString(databaseProductName);
            if( string.IsNullOrWhiteSpace(ConnectionString))
                throw new NullReferenceException($"Did not find the connectionstring for \"{databaseProductName}\"");

            var log4NetConfigFilename = $"{databaseProductName}-{tableAndConfigurationName}.config";
            var fileInfo = new FileInfo(log4NetConfigFilename);
            if (fileInfo.Exists == false)
                throw new FileNotFoundException($"Did not find log4net configuration file \"{log4NetConfigFilename}\"");

            XmlConfigurator.Configure(repository, fileInfo);

            if(repository.Configured == false )
                throw new InvalidOperationException("Repository not configured");
            if( repository.ConfigurationMessages.Count != 0)
                throw new InvalidOperationException($"Repository messages found: {string.Join(",", repository.ConfigurationMessages)}");

            foreach (var appender in repository.GetAppenders())
            {
                if (appender is AdoNetAppender adoNetAppender)
                {
                    adoNetAppender.CommandText = string.Format(adoNetAppender.CommandText, tableAndConfigurationName);
                    adoNetAppender.ConnectionString = ConnectionString;
                    adoNetAppender.ErrorHandler = this;
                    adoNetAppender.ActivateOptions();
                }
            }

            return _log4netErrors;
        }
    }
}