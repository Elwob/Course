using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities
{
    public class Course_ParticipantsEntities : DbContext
    {

        public DbSet<Course_Participants> Course_Participants { get; set; }  //Is the Name That is Called
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=dcv;user=Root");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);




            modelBuilder.Entity<Course_Participants>(entity =>
                {
                    entity.HasKey(x => x.id);
                    entity.Property(x => x.course_id).IsRequired();
                    entity.Property(x => x.participant_id).IsRequired();
                    entity.HasOne(x=>x.)


                });
        }
    } }