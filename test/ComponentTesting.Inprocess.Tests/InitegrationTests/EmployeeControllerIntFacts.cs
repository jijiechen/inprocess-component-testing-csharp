using System;
using ComponentTesting.Inprocess.App;
using ComponentTesting.Inprocess.Data;
using ComponentTesting.Inprocess.Models;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ComponentTesting.Inprocess.Tests.InitegrationTests
{
    public class EmployeeControllerIntFacts
    {
        
        [Fact(Skip = "This case replies on real database instances")]
        public async void should_handle_search_employee_request()
        {
            var server = new TestServer(WebApplication.CreateWebHost());
            var client = server.CreateClient();

            var response = await client.GetAsync("/employees/search/im");
            var employeeString = await response.Content.ReadAsStringAsync();
            
            Assert.Contains("Jim", employeeString);
        }
        
        
        [Fact]
        public async void should_handle_search_request_with_mocked_database()
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

            IServiceProvider appServices = null;
            var server = new TestServer(WebApplication.CreateWebHost(services =>
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlite(connection);
                });
            }, null, app => appServices = app.ApplicationServices));

            var jim = new Employee {Id = 12, Name = "Jim"};
            appServices.GetService<IRepository<Employee>>().Save(jim);
            
            var client = server.CreateClient();

            var response = await client.GetAsync("/employees/search/im");
            var employeeString = await response.Content.ReadAsStringAsync();
            
            Assert.Equal("Jim(id=12)", employeeString);
        }
    }
}