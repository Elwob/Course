using Data.Models;
using Data.Models.Relations;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities
{
    /// <summary>
    /// contains all data needed to manage courses from DataBase "dcv"
    /// </summary>
    public class CourseEntities : DbContext
    {
        /// <summary>
        /// contains all courses existing in DB
        /// </summary>
        public DbSet<Course> Courses { get; set; }

        /// <summary>
        /// contains all teaching contents existing in DB
        /// </summary>
        public DbSet<Content> Contents { get; set; }

        /// <summary>
        /// contains all subventions existing in DB
        /// </summary>
        public DbSet<Subvention> Subventions { get; set; }

        /// <summary>
        /// contains all documents existing in DB
        /// </summary>
        public DbSet<Document> Documents { get; set; }

        /// <summary>
        /// contains all communication records existing in DB
        /// </summary>
        public DbSet<Communication> Communications { get; set; }

        /// <summary>
        /// contains all absences existing in DB
        /// </summary>
        public DbSet<Absence> Absences { get; set; }

        /// <summary>
        /// contains all templates for automatic document creation existing in DB
        /// </summary>
        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        /// <summary>
        /// contains core data for all persons in DB
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// contains all addresses existing in DB
        /// </summary>
        public DbSet<Address> Addresses { get; set; }

        /// <summary>
        /// contains all contact options existing in DB
        /// </summary>
        public DbSet<Contact> Contacts { get; set; }

        /// <summary>
        /// contains all comments for persons existing in DB
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        /// contains all classrooms existing in DB
        /// </summary>
        public DbSet<Classroom> Classrooms { get; set; }

        /// <summary>
        /// contains all notebooks existing in DB
        /// </summary>
        public DbSet<Notebook> Notebooks { get; set; }

        /// <summary>
        /// contains all Equipment existing in DB
        /// </summary>
        public DbSet<Equipment> Equipments { get; set; }

        /// contains all possible course categories
        /// </summary>
        public DbSet<CourseCategory> CourseCategories { get; set; }

        /// <summary>
        /// contains all relations between courses and contents existing in DB
        /// </summary>
        public DbSet<RelCourseContent> RelCourseContents { get; set; }

        /// <summary>
        /// contains all relations between courses and subventions existing in DB
        /// </summary>
        public DbSet<RelCourseSubvention> RelCourseSubventions { get; set; }

        /// <summary>
        /// contains all relations between documents and classes (like Person, Course, etc.) existing in DB
        /// </summary>
        public DbSet<RelDocumentClass> RelDocumentClasses { get; set; }

        /// <summary>
        /// contains all relations between communications and classes (like Person, Course, etc.) existing in DB
        /// </summary>
        public DbSet<RelCommunicationClass> RelCommunicationClasses { get; set; }

        /// <summary>
        /// contains all relations between courses and participants existing in DB
        /// </summary>
        public DbSet<RelCourseParticipant> RelCourseParticipants { get; set; }

        /// <summary>
        /// contains all relations between courses and trainers existing in DB
        /// </summary>
        public DbSet<RelCourseTrainer> RelCourseTrainers { get; set; }

        /// <summary>
        /// contains all relations between addresses and persons existing in DB
        /// </summary>
        public DbSet<RelAddressPerson> RelAddressPersons { get; set; }

        /// <summary>
        /// contains all relations between classrooms and addresses
        /// </summary>
        public DbSet<RelClassroomAddress> RelClassroomAddresses { get; set; }

        /// <summary>
        /// contains all relations between courses and classrooms
        /// </summary>
        public DbSet<RelCourseClassroom> RelCourseClassrooms { get; set; }

        /// <summary>
        /// the singleton instance handed if method CourseEntities.GetInstance() is called
        /// </summary>
        private static CourseEntities instance = null;

        /// <summary>
        /// returns the singleton instance of CourseEntities
        /// </summary>
        /// <returns></returns>
        public static CourseEntities GetInstance()
        {
            if (instance == null)
            {
                return new CourseEntities();
            }
            else
            {
                return instance;
            }
        }

        /// <summary>
        /// creates a DBContextOptionsBuilder to represent the DCV-dataBase
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// set to "server=192.168.0.94;database=dcv;user=root" if used with DB on server
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=192.168.0.94;database=dcv;user=root");
        }

        /// <summary>
        /// creates the model to represent the DCV-database
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            CreateCourseSpecificModels(modelBuilder);
            CreatePersonSpecificModels(modelBuilder);
            CreateCourseRelations(modelBuilder);
            CreatePersonRelations(modelBuilder);
        }

        /// <summary>
        /// creates primary course models
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void CreateCourseSpecificModels(ModelBuilder modelBuilder)
        {
            // builds model Course
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).IsRequired();
                entity.Property(x => x.Title).IsRequired();
                entity.Property(x => x.Category).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();
            });
            // represents the model Subvention
            modelBuilder.Entity<Subvention>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
            });
            // builds model Document
            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Url).IsRequired();
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();
                entity.Property(x => x.Type).IsRequired();
            });
            // represents the model Absence
            modelBuilder.Entity<Absence>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Start).IsRequired();
                entity.Property(x => x.ParticipantId).IsRequired();
                entity.Property(x => x.CourseId).IsRequired();
                entity.Property(x => x.Completed).IsRequired();
                // connection to a course
                entity.HasOne(c => c.Course)
                .WithMany(a => a.Absences)
                .HasForeignKey(a => a.CourseId);
                // connection to a participant (Person)
                entity.HasOne(p => p.Person)
                .WithMany(a => a.Absences)
                .HasForeignKey(a => a.ParticipantId);
                // connection to a document
                entity.HasMany(d => d.Documents)
                .WithOne(a => a.Absence)
                .HasForeignKey(a => a.Id);
            });
            // represents the model EmailTemplate
            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.DocumentType).IsRequired();
            });
            // represents the model Classroom
            modelBuilder.Entity<Classroom>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
            // represents the model
            modelBuilder.Entity<CourseCategory>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Color).IsRequired();
                entity.Property(x => x.FontColor).IsRequired();
            });
        }

        /// <summary>
        /// creates primary person models
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void CreatePersonSpecificModels(ModelBuilder modelBuilder)
        {
            // represents the model Person (core data)
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.LastName).IsRequired();
                entity.Property(x => x.Active).IsRequired();
                entity.Property(x => x.Deleted).IsRequired();
                entity.Property(x => x.WantsNewsletter).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();
            });
            // represents the model Address
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.CreatedAt).IsRequired();
            });
            // represents the model Contact
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.PersonId).IsRequired();
                entity.Property(x => x.ArtOfCommunication).IsRequired();
                entity.Property(x => x.ContactValue).IsRequired();
                entity.Property(x => x.ContactType).IsRequired();
                entity.Property(x => x.MainContact).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();
                // connection to a person
                entity.HasOne(x => x.Person)
                .WithMany(x => x.Contacts)
                .HasForeignKey(x => x.PersonId);
            });
            // builds model Communication
            modelBuilder.Entity<Communication>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Channel).IsRequired();
                entity.Property(x => x.PersonId).IsRequired();
                entity.Property(x => x.Date).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();
                // connections to document and person
                entity.HasOne(x => x.Document);
                entity.HasOne(x => x.Person);
            });
            // represents the model Comment
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.PersonId).IsRequired();
                entity.Property(x => x.CommentValue).IsRequired();
                entity.Property(x => x.ValueDate).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();
                // connection to a person
                entity.HasOne(x => x.Person).WithMany(x => x.Comments).HasForeignKey(x => x.PersonId);
            });
            // represents the model Notebook
            modelBuilder.Entity<Notebook>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.SerialNumber).IsRequired();
                entity.Property(x => x.Make).IsRequired();
                entity.Property(x => x.Model).IsRequired();
                // connection between Notebook and Person
                entity.HasOne(x => x.Person);
                // connection between Notebook and Location
                entity.HasOne(x => x.Location);
            });
            // represents the model Notebook
            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Type).IsRequired();
                entity.Property(x => x.Quantity).IsRequired();
                // connection between Equipment and Person
                entity.HasOne(x => x.Person);
                // connection between Equipment and Location
                entity.HasOne(x => x.Location);
            });
        }

        /// <summary>
        /// creates primarily relations between person models + classroom
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void CreatePersonRelations(ModelBuilder modelBuilder)
        {
            // represents the n:m relation document-classes
            modelBuilder.Entity<RelDocumentClass>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.DocId).IsRequired();
                entity.Property(x => x.Class).IsRequired();
                entity.Property(x => x.ClassId).IsRequired();
                // connection between document and different classes (e.g. Person, Course)
                entity.HasOne(x => x.Document)
                .WithMany(x => x.DocumentClasses)
                .HasForeignKey(x => x.DocId);
            });
            // represents the n:m relation communication-classes
            modelBuilder.Entity<RelCommunicationClass>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.CommunicationId).IsRequired();
                entity.Property(x => x.Class).IsRequired();
                entity.Property(x => x.ClassId).IsRequired();
                // connection between communication and different classes (e.g. Person, Course)
                entity.HasOne(x => x.Communication)
                .WithMany(x => x.CommunicationClasses)
                .HasForeignKey(x => x.CommunicationId);
            });
            // represents the n:m relation between addresses and persons
            modelBuilder.Entity<RelAddressPerson>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.AddressId).IsRequired();
                entity.Property(x => x.PersonId).IsRequired();
                // connection to n persons
                entity.HasOne(c => c.Person).
                WithMany(co => co.AddressPersons)
                .HasForeignKey(c => c.PersonId);
                // connection to n addresses
                entity.HasOne(co => co.Address)
                .WithMany(c => c.AddressPersons)
                .HasForeignKey(co => co.AddressId);
            });
            // represents the relations between addresses and classrooms
            // foreign keys are not built in entity model builder (not needed for our purpose)
            modelBuilder.Entity<RelClassroomAddress>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.AddressId).IsRequired();
                entity.Property(x => x.LocationId).IsRequired();
            });
        }

        /// <summary>
        /// creates primarily relations between course models as well as course-person relations
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void CreateCourseRelations(ModelBuilder modelBuilder)
        {
            // represents the n:m relation course-participants
            modelBuilder.Entity<RelCourseParticipant>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).IsRequired();
                entity.Property(x => x.Completed).IsRequired();
                // connection to n courses
                entity.HasOne(c => c.Course)
                .WithMany(r => r.CourseParticipants)
                .HasForeignKey(r => r.CourseId);
                // connection to n participants (Person)
                entity.HasOne(p => p.Person)
                .WithMany(r => r.RelCourseParticipants)
                .HasForeignKey(r => r.ParticipantId);
            });
            // represents the n:m relaton between courses and contents
            modelBuilder.Entity<RelCourseContent>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.CourseId).IsRequired();
                entity.Property(x => x.ContentId).IsRequired();
                // connection to n courses
                entity.HasOne(c => c.Course)
                .WithMany(co => co.CourseContents)
                .HasForeignKey(c => c.CourseId);
                // connection to n contents
                entity.HasOne(co => co.Content)
                .WithMany(c => c.ContentCourse)
                .HasForeignKey(co => co.ContentId);
            });
            // represents the n:m relation between courses and subventions
            modelBuilder.Entity<RelCourseSubvention>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.CourseId).IsRequired();
                entity.Property(x => x.SubventionId).IsRequired();
                entity.Property(x => x.CourseId).IsRequired();
                // connection to n courses
                entity.HasOne(c => c.Course)
                .WithMany(sub => sub.CourseSubventions)
                .HasForeignKey(c => c.CourseId);
                // connection to n subventions
                entity.HasOne(sub => sub.Subvention)
                .WithMany(c => c.CourseSubventions)
                .HasForeignKey(sub => sub.SubventionId);
            });
            // represents the n:m relation between courses and trainers
            modelBuilder.Entity<RelCourseTrainer>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.CourseId).IsRequired();
                entity.Property(x => x.TrainerId).IsRequired();
                // connection to n courses
                entity.HasOne(c => c.Course)
                .WithMany(r => r.CourseTrainers)
                .HasForeignKey(r => r.CourseId);
                // connection to n trainers (Person)
                entity.HasOne(t => t.Trainer)
                .WithMany(r => r.RelCourseTrainers)
                .HasForeignKey(t => t.TrainerId);
            });
            // represents the n:m relation between courses and classrooms
            modelBuilder.Entity<RelCourseClassroom>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.CourseId).IsRequired();
                entity.Property(x => x.ClassroomId).IsRequired();
                // connection to n courses
                entity.HasOne(c => c.Course)
                .WithMany(r => r.CourseClassrooms)
                .HasForeignKey(c => c.CourseId);
                // connection to n classrooms
                entity.HasOne(c => c.Classroom)
                .WithMany(r => r.ClassroomCourses)
                .HasForeignKey(c => c.ClassroomId);
            });
        }
    }
}