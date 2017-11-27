using ComponentTesting.Inprocess.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ComponentTesting.Inprocess.Tests
{
    public class DatabaseUtils
    {
        public static SqliteConnection CreateInMemorryDatabase(out DbContextOptions<ApplicationDbContext> dbOptions)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlite(connection);
            
            dbOptions = builder.Options;
            using (var context = new ApplicationDbContext(builder.Options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
            }

            return connection;
        }
    }
}