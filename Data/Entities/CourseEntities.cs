using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class CourseEntities : DbContext
    {
        public DbSet<Course> Course { get; set; }
        public DbSet<Content> Content { get; set; }


        public DbSet<RelCourseParticipant> RelCourseParticipants{ get; set; }
        public DbSet<RelCourseTrainer> RelCourseTrainers { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=192.168.0.94;database=dcv;user=root");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Title).IsRequired();
                entity.Property(x => x.Category).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();
            });

            modelBuilder.Entity<Content>(entity =>
            {
                entity.HasKey(x => x.id);
            });








            modelBuilder.Entity<RelCourseParticipant>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.CourseId).IsRequired();
                entity.Property(x => x.ParticipantId).IsRequired();
             
            });

            modelBuilder.Entity<RelCourseTrainer>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.CourseId).IsRequired();
                entity.Property(x => x.TrainerID).IsRequired();

            });


            modelBuilder.Entity<Absence>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Start).IsRequired();
                entity.Property(x => x.ParticipantId).IsRequired();
                entity.Property(x => x.CourseId).IsRequired();
                entity.Property(x => x.Completed).IsRequired();


            });

            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.DocumentType).IsRequired();
                entity.Property(x => x.Text).IsRequired();

            });

        }
    }
}
