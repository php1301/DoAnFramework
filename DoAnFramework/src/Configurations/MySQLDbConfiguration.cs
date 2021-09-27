using System.Configuration;
using System.Data;
using System.Data.Entity;
using MySql.Data.EntityFramework;
using MySql.Data.MySqlClient;

namespace DoAnFramework.src.Configurations
{
    public class MySQLDbConfiguration: DbConfiguration
	{
		
			public MySQLDbConfiguration()
			{
				var dataSet = (DataSet)ConfigurationManager.GetSection("system.data");
				dataSet.Tables[0].Rows.Add(
					"MySQL Data Provider",
					".Net Framework Data Provider for MySQL",
					"MySql.Data.MySqlClient",
					typeof(MySqlClientFactory).AssemblyQualifiedName
				);

				SetProviderServices("MySql.Data.MySqlClient", new MySqlProviderServices());
				SetDefaultConnectionFactory(new MySqlConnectionFactory());
			}
	}
}
