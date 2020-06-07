using System;
using System.Linq;
using log4net;
using MicroKnights.Log4NetAdoNetAppender.Test.Fixtures;
using Xunit;

// ReSharper disable PossibleMultipleEnumeration

namespace MicroKnights.Log4NetAdoNetAppender.Test.Tests
{
    public class TestPostgreSql : IClassFixture<PostgreSqlFixture>
    {
        private readonly PostgreSqlFixture _fixture;

        public TestPostgreSql(PostgreSqlFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void TestNullable()
        {
            var errors = _fixture.InitializeLog4Net("log4netnullable");

            var log = LogManager.GetLogger(typeof(TestPostgreSql));
            LogicalThreadContext.Properties["Number"] = 42;
            log.Info("bla bla");
            Assert.False(errors.Any(), errors.FirstOrDefault());

            log.Error("bla bla", new Exception());
            Assert.False(errors.Any(), errors.FirstOrDefault());

            Assert.True(true);
        }

        [Fact]
        public void TestNotNullable()
        {
            var errors = _fixture.InitializeLog4Net("log4netnotnullable");

            var log = LogManager.GetLogger(typeof(TestPostgreSql));
            LogicalThreadContext.Properties["Number"] = 42;
            log.Info("bla bla");
            Assert.False(errors.Any(), errors.FirstOrDefault());

            log.Error("bla bla", new Exception());
            Assert.False(errors.Any(), errors.FirstOrDefault());

            Assert.True(true);
        }
    }
}