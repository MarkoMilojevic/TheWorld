using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebApp.Models
{
    public class WorldContext : DbContext
    {
        private readonly IConfigurationRoot _config;

        public WorldContext(IConfigurationRoot config, DbContextOptions options)
                : base(options)
        {
            _config = config;
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Trip> Stops { get; set; }

        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:WorldContextConnection"]);
        }
    }
}
