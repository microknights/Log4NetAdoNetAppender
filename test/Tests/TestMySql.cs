using System;
using System.Linq;
using log4net;
using MicroKnights.Log4NetAdoNetAppender.Test.Fixtures;
using Xunit;

namespace MicroKnights.Log4NetAdoNetAppender.Test.Tests
{
    public class TestMySql : IClassFixture<MySqlFixture>
    {
        private readonly MySqlFixture _fixture;

        public TestMySql(MySqlFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void TestNullable()
        {
            var errors = _fixture.InitializeLog4Net("Log4NetNullable");
            Assert.False(errors.Any(), errors.FirstOrDefault());

            var log = LogManager.GetLogger(typeof(TestMySql));
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
            var errors = _fixture.InitializeLog4Net("Log4NetNotNullable");
            Assert.Empty(errors);

            var log = LogManager.GetLogger(typeof(TestMySql));
            LogicalThreadContext.Properties["Number"] = 42;
            log.Info("bla bla");
            Assert.False(errors.Any(), errors.FirstOrDefault());

            log.Error("bla bla", new Exception());
            Assert.False(errors.Any(), errors.FirstOrDefault());

            Assert.True(true);
        }
    }
}