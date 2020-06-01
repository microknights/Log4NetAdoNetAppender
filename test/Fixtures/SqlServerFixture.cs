using Microsoft.Extensions.Configuration;

namespace MicroKnights.Log4NetAdoNetAppender.Test.Fixtures
{
    public class SqlServerFixture : DatabaseFixture
    {
        protected override string DatabaseProductName => "sqlserver";
    }
}