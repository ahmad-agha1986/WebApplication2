using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class MyDatabaseContext : DbContext
    {


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "MyDatabase.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");

        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserAuth> UserAuths { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //relational keys User & UserAuth
            modelBuilder.Entity<User>()
            .HasOne(usr => usr.UserAuth)
            .WithMany(ua => ua.User)
            .HasForeignKey(usr => usr.UserAuth_Id)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<UserRoles>()
             .HasOne(uR => uR.UserAuth)
             .WithMany(uu => uu.UserRole)
             .HasForeignKey(uR => uR.UserAuthId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRoles>()
                .HasOne(uRr => uRr.Role)
                .WithMany(rr => rr.UserRoles)
                .HasForeignKey(uRr => uRr.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

        }



    }

}