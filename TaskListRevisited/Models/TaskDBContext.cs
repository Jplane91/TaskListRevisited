using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TaskListRevisited.Models
{
    public partial class TaskDBContext : DbContext
    {
        public TaskDBContext()
        {
        }

        public TaskDBContext(DbContextOptions<TaskDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TaskInfo> TaskInfo { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database= TaskDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<TaskInfo>(entity =>
            {
                entity.HasKey(e => e.TaskNumber)
                    .HasName("PK__TaskInfo__6601A97DD32E8212");

                entity.Property(e => e.TaskDesc)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TaskDueDate).HasColumnType("date");

                entity.Property(e => e.TaskStatus).HasMaxLength(20);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TaskInfo)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__TaskInfo__UserId__4BAC3F29");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserInfo__1788CCAC76071A95");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(30);
            });
        }
    }
}
