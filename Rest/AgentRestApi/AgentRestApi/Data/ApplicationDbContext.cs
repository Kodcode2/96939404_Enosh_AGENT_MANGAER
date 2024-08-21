using AgentRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentRestApi.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IConfiguration configuration) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }


        public DbSet<AgentModel> Agents { get; set; }
        public DbSet<TargetModel> Targets { get; set; }
        public DbSet<MissionModel> Missions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MissionModel>()
                .HasOne(m => m.AgentModel)
                .WithMany()
                .HasForeignKey(m => m.AgentId);

            modelBuilder.Entity<MissionModel>()
                .HasOne(m => m.TargetModel)
                .WithMany()
                .HasForeignKey(m => m.TargetId);

            base.OnModelCreating(modelBuilder);
        }



    }
}
