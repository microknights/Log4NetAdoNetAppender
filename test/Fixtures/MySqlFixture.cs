namespace MicroKnights.Log4NetAdoNetAppender.Test.Fixtures
{
    public class MySqlFixture : DatabaseFixture
    {
        protected override string DatabaseProductName => "mysql";
    }
}