using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Data.Entities
{
    public class AbsenceEntitie : DbContext
    {
        public DbSet<Absence> Absence { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=dcv;user=Root");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Absence>(entity =>
           {
               entity.HasKey(x => x.id  );
               entity.Property(x => x.start).IsRequired();
               entity.Property(x => x.participant_id).IsRequired();
               entity.Property(x => x.course_id).IsRequired();
               entity.Property(x => x.completed).IsRequired();
           });
        }
    }
}