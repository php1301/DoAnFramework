using EntityFramework.BulkInsert;

namespace  DoAnFramework.src.Configurations
{

    public static class EfBulkInsertGlimpseProviderRegistry
    {
        public static void Execute()
        {
            ProviderFactory.Register<GlimpseConnectionEfSqlBulkInsertProvider>("Glimpse.Ado.AlternateType.GlimpseDbConnection");
        }
    }
}
