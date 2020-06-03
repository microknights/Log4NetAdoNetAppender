namespace MicroKnights.Log4NetAdoNetAppender.Test.Fixtures
{
    public class PostgreSqlFixture : DatabaseFixture
    {
        protected override string DatabaseProductName => "postgresql";
    }
}