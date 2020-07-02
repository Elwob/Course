using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities
{
    public class CourseEntities : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Content> Subventions { get; set; }
        public DbSet<RelCourseContent> RelCourseContents { get; set; }
        public DbSet<RelCourseSubvention> RelCourseSubventions { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<RelDocumentClass> RelDocumentClasses { get; set; }
        public DbSet<Communication> Communications { get; set; }
        public DbSet<RelCommunicationClass> RelCommunicationClasses { get; set; }
        public DbSet<RelCourseParticipant> RelCourseParticipants { get; set; }
        public DbSet<RelCourseTrainer> RelCourseTrainers { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<RelAddressPerson> RelAddressPersons { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Comment> Comments { get; set; }


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

            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Url).IsRequired();
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();
                entity.Property(x => x.Type).IsRequired();
            });

            modelBuilder.Entity<Communication>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Channel).IsRequired();
                entity.Property(x => x.PersonId).IsRequired();
                entity.Property(x => x.Date).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();
            });
            modelBuilder.Entity<RelCommunicationClass>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.CommunicationId).IsRequired();
                entity.Property(x => x.Class).IsRequired();
                entity.Property(x => x.ClassId).IsRequired();
            });

            modelBuilder.Entity<RelCourseParticipant>(entity =>
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

            modelBuilder.Entity<RelDocumentClass>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.DocId).IsRequired();
                entity.Property(x => x.Class).IsRequired();
                entity.Property(x => x.ClassId).IsRequired();
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.FirstName).IsRequired();
                entity.Property(x => x.LastName).IsRequired();
                entity.Property(x => x.Function).IsRequired();
                entity.Property(x => x.Active).IsRequired();
                entity.Property(x => x.Deleted).IsRequired();
                entity.Property(x => x.WantsNewsletter).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.CreatedAt).IsRequired();
            });

            modelBuilder.Entity<RelAddressPerson>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.AdressId).IsRequired();
                entity.Property(x => x.PersonId).IsRequired();
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.PersonId).IsRequired();
                entity.Property(x => x.ArtOfCommunication).IsRequired();
                entity.Property(x => x.ContactValue).IsRequired();
                entity.Property(x => x.ContactType).IsRequired();
                entity.Property(x => x.MainContact).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.PersonId).IsRequired();
                entity.Property(x => x.CommentValue).IsRequired();
                entity.Property(x => x.ValueDate).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();

            });
        }
    }
}