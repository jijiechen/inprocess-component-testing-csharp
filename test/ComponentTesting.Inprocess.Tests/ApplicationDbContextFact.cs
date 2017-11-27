using System.Linq;
using ComponentTesting.Inprocess.Data;
using ComponentTesting.Inprocess.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ComponentTesting.Inprocess.Tests
{
    public class ApplicationDbContextFact
    {
        [Fact]
        public void should_save_employees()
        {
            var databaseOptions = InitInMemorryDatabase();
            
            var testEntity = new Employee(){ Name = "Jim", Id = 2};
            CreateEmployee(testEntity, databaseOptions);
            
            var foundEmployee = FindEmployee(testEntity.Id, databaseOptions); 
            
            Assert.NotNull(foundEmployee);
            Assert.Equal(testEntity.Name, foundEmployee.Name);
        }

        private static DbContextOptions<ApplicationDbContext> InitInMemorryDatabase()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlite(connection);
            
            using (var context = new ApplicationDbContext(builder.Options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
            }

            return builder.Options;
        }

        private static void CreateEmployee(Employee testEntity, DbContextOptions<ApplicationDbContext> options)
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.Set<Employee>().Add(testEntity);
                context.SaveChanges();
            }
        }

        private static Employee FindEmployee(long testEntityId, DbContextOptions<ApplicationDbContext> options)
        {
            Employee foundEmployee;
            using (var context = new ApplicationDbContext(options))
            {
                foundEmployee = context.Set<Employee>().FirstOrDefault(x => x.Id == testEntityId);
            }
            return foundEmployee;
        }
    }
}