using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;

namespace DbSync
{
    public class SynchronizationService
    {
        public const string VERSION_TABLE = "VersionInfo";

        protected readonly string ConnectionString;

        public SynchronizationService(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void RunMigrations(Assembly migrationsAssembly)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                AssertVersionTableExists(connection);

                var migrations = from t in migrationsAssembly.GetTypes()
                                 where typeof(IMigration).IsAssignableFrom(t)
                                 let instance = (IMigration)Activator.CreateInstance(t)
                                 orderby instance.Version ascending
                                 select instance;

                foreach (var m in migrations)
                {
                    RunMigrationIfNecessary(connection, m);
                }

                connection.Close();
            }
        }

        #region Support

        protected void AssertVersionTableExists(SqlConnection connection)
        {
            var command = new SqlCommand(@"
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + VERSION_TABLE + @"'))
BEGIN
    CREATE TABLE " + VERSION_TABLE + @"
    (
	    MigrationId BIGINT NOT NULL,
	    AppliedOn DATETIME NOT NULL,
	    CONSTRAINT PK_VersionInfo PRIMARY KEY(MigrationId)
    )
END", connection);
            var result = command.ExecuteNonQuery();
        }

        protected void RunMigrationIfNecessary(SqlConnection connection, IMigration migration)
        {   
            // Determine if we need to apply this one
            var exists = new SqlCommand("SELECT COUNT(*) FROM " + VERSION_TABLE + " WHERE MigrationId = " + migration.Version, connection);
            var results = (int)exists.ExecuteScalar();
            if(results != 0)
            {
                // Already applied
                return;
            }
         
            // Execute the migration
            var command = new SqlCommand(migration.SqlCommand, connection);
            command.ExecuteNonQuery();

            // Add a record to the version table
            var addVersion = new SqlCommand("INSERT INTO " + VERSION_TABLE + " SELECT " + migration.Version + ", GETUTCDATE()", connection);
            addVersion.ExecuteNonQuery();
        }

        #endregion
    }
}
