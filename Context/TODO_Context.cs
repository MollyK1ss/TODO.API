using Microsoft.EntityFrameworkCore;
using TODO.API.Models;

namespace TODO.API.Context
{
    public class TODO_Context : DbContext
    {
        public TODO_Context(DbContextOptions<TODO_Context> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TODOModel>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.CreatedTask).HasColumnType("date");
                entity.Property(x => x.EndTask).HasColumnType("date");
                entity.Property(x => x.UpdateTask).HasColumnType("date");
                entity.Property(x => x.Description).IsRequired(false);
            });

            modelBuilder.Entity<PersonModel>(entity => 
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Patronymic).IsRequired(false);
                entity.Property(x => x.DateBirth).HasColumnType("date");
                entity.HasMany(x => x.tODOModels).WithOne()
                    .HasForeignKey(x => x.PersonId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(x => x.Document).WithOne()
                    .HasForeignKey(nameof(DocumentModel), nameof(DocumentModel.PersonId)).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DocumentModel>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.DateIssue).HasColumnType("date");
            });
        }

        public DbSet<TODOModel> TODOTable { get; set; }
        public DbSet<PersonModel> Person { get; set; }
        public DbSet<DocumentModel> Document { get; set; }
    }
}
