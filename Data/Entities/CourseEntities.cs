using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class CourseEntities : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Content> Subventions { get; set; }
        public DbSet<RelCourseContent> RelCourseContents { get; set; }
        public DbSet<RelCourseSubvention> RelCourseSubventions { get; set; }

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
                entity.Property(x => x.Id).IsRequired();
                entity.Property(x => x.Title).IsRequired();
                entity.Property(x => x.Category).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();
            });

            modelBuilder.Entity<Content>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).IsRequired();
            });

            modelBuilder.Entity<Subvention>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
            });

            modelBuilder.Entity<RelCourseContent>(entity =>
            {
                entity.HasKey(x => new { x.Id, x.CourseId, x.ContentId });
                entity.Property(x => x.CourseId).IsRequired();
                entity.Property(x => x.ContentId).IsRequired();

                entity.HasOne(c => c.Course)
                .WithMany(co => co.CourseContents)
                .HasForeignKey(c => c.CourseId);

                entity.HasOne(co => co.Content)
                .WithMany(c => c.CourseContents)
                .HasForeignKey(co => co.ContentId);
            });

            modelBuilder.Entity<RelCourseSubvention>(entity =>
            {
                entity.HasKey(x => new { x.Id, x.CourseId, x.SubventionId });
                entity.Property(x => x.CourseId).IsRequired();
                entity.Property(x => x.SubventionId).IsRequired();
                entity.HasOne(c => c.Course)
                        .WithMany(sub => sub.CourseSubventions)
                        .HasForeignKey(c => c.CourseId);
                entity.HasOne(sub => 
                            sub.Subvention)
                        .WithMany(c => 
                            c.CourseSubventions)
                        .HasForeignKey(sub => 
                            sub.SubventionId);
            });
        }
    }
}
