using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Netblaster.Hermes.DAL.Model;

namespace Netblaster.Hermes.DAL
{
    public class HermesDataContext : IdentityDbContext<ApplicationUser>
    {
        public static HermesDataContext Create()
        {
            return new HermesDataContext();
        }

        public HermesDataContext() : base("HermesDataContext")
        {
            Database.SetInitializer<HermesDataContext>(null);// Remove default initializer
        }

        public DbSet<CalendarComponent> CalendarComponents { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public DbSet<WorkTime> WorkTimes { get; set; }

        public DbSet<Parameter> Parameters { get; set; }

        public DbSet<TaskDetail> TaskDetails { get; set; }

        public DbSet<ChatItem> ChatItems { get; set; }

        public DbSet<UserMessage> UserMessages { get; set; }

        public DbSet<TaskItemUser> TaskItemUsers { get; set; }

        public DbSet<Attachment> Attachments { get; set; }

        public DbSet<TaskSubItem> TaskSubItems { get; set; }

        public DbSet<UserGroup> UserGroups { get; set; }

        public DbSet<Group> Groups { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Configure Asp Net Identity Tables
            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<ApplicationUser>().Property(u => u.PasswordHash).HasMaxLength(500);
            modelBuilder.Entity<ApplicationUser>().Property(u => u.PhoneNumber).HasMaxLength(50);


            modelBuilder.Entity<TaskItem>()
                .HasRequired(c => c.CreatedBy)
                .WithMany()
                .WillCascadeOnDelete(false);

        }
    }
}
