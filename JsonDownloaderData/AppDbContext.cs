using JsonDownloaderData.Data;
using Microsoft.EntityFrameworkCore;

using Npgsql;

namespace JsonDownloaderData
{
    public class AppDbContext: DbContext
    {
        public DbSet<JsonItem> JsonItems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("PgDbContextConnection");
            var builder = new NpgsqlConnectionStringBuilder(connectionString);
            optionsBuilder.UseNpgsql(builder.ConnectionString);
        }
    }
}
