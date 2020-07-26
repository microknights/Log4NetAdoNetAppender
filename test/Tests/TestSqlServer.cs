using System;
using System.Linq;
using log4net;
using MicroKnights.Log4NetAdoNetAppender.Test.Fixtures;
using Xunit;

// ReSharper disable PossibleMultipleEnumeration

namespace MicroKnights.Log4NetAdoNetAppender.Test.Tests
{
    public class TestSqlServer : IClassFixture<SqlServerFixture>
    {
        private readonly SqlServerFixture _fixture;

        public TestSqlServer(SqlServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void TestNullable()
        {
            var errors = _fixture.InitializeLog4Net("log4netnullable");
            Assert.Empty(errors);

            var log = LogManager.GetLogger(typeof(TestSqlServer));
            log.Info("bla bla");
            Assert.False(errors.Any(),errors.FirstOrDefault());

            log.Error("bla bla",new Exception());
            Assert.False(errors.Any(), errors.FirstOrDefault());

            Assert.True(true);
        }

        [Fact]
        public void TestNotNullable()
        {
            var errors = _fixture.InitializeLog4Net("log4netnotnullable");
            Assert.Empty(errors);

            var log = LogManager.GetLogger(typeof(TestSqlServer));
            log.Info("bla bla");
            Assert.False(errors.Any(), errors.FirstOrDefault());

            log.Error("bla bla", new Exception());
            Assert.False(errors.Any(), errors.FirstOrDefault());

            Assert.True(true);
        }

        [Fact]
        public void TestBuffering25()
        {
            var errors = _fixture.InitializeLog4Net("log4netbuffering25");
            Assert.Empty(errors);

            var log = LogManager.GetLogger(typeof(TestSqlServer));
            for (int i = 0; i < 30; i++)
            {
                log.Info($"bla bla: {i}");
            }
            Assert.False(errors.Any(), errors.FirstOrDefault());
        }

    }

}
