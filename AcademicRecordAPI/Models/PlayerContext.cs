using System;
using Microsoft.EntityFrameworkCore;

namespace AcademicRecordAPI.Models
{
    public class PlayerContext : DbContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options) : base(options)
        {}

        public DbSet<Player> Players { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        //public SubjectContext(DbContextOptions<SubjectContext> options) : base(options)
        //{ }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Subject>()
                .Property<int>("PlayerForeignKey");

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Player)
                .WithMany(p => p.Subjects)
                .HasForeignKey(s => s.PlayerForeignKey);
        }
    }
}
