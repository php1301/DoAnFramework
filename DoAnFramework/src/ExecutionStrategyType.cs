
namespace DoAnFramework
{
    public enum ExecutionStrategyType
    {
        NotFound = 0,
        CompileExecuteAndCheck = 1,
        RemoteExecution = 2,
        CheckOnly = 3,
        MySqlPrepareDatabaseAndRunQueries = 4,
        MySqlRunQueriesAndCheckDatabase = 5,
        MySqlRunSkeletonRunQueriesAndCheckDatabase = 6,
        DoNothing = 7,
        SqlServerSingleDatabasePrepareDatabaseAndRunQueries = 8,
        SqlServerSingleDatabaseRunQueriesAndCheckDatabase = 9,
        SqlServerSingleDatabaseRunSkeletonRunQueriesAndCheckDatabase = 10,
    }
}
