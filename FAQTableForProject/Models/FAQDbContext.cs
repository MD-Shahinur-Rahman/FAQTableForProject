using Microsoft.EntityFrameworkCore;

namespace FAQTableForProject.Models
{
    public class FAQDbContext:DbContext
    {
        public FAQDbContext(DbContextOptions<FAQDbContext>options):base(options)
        {
            
        }

        public DbSet<FAQ> FAQs { get; set; } = default!;
        public DbSet<FAQCategory> FAQsCategory { get; set; } = default!;
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Term> Terms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Partner>(entity =>
            {
                //entity.Property(e => e.PartnerName)
                //    .IsRequired()
                //    .HasMaxLength(100);

                //entity.Property(e => e.Email)
                //    .IsRequired()
                //    .HasMaxLength(100);

                //entity.Property(e => e.ContactNumber)
                //    .IsRequired()
                //    .HasMaxLength(20);

                //entity.Property(e => e.City)
                //    .IsRequired()
                //    .HasMaxLength(100);

                //entity.Property(e => e.PostCode)
                //    .IsRequired()
                //    .HasDefaultValue(0);

                //entity.Property(e => e.PhotoName)
                //    .HasMaxLength(200);

                //entity.Property(e => e.AgreementSignDate)
                //    .IsRequired();

                //entity.Property(e => e.AgreementEndDate)
                //    .IsRequired();

                entity.Property(e => e.AgreementTotal)
                    .IsRequired()
                    .HasDefaultValue(0);
                entity.Property(e => e.AgreementTotal)
                    .IsRequired()
                    .HasColumnType("decimal(18, 2)");

                //entity.Property(e => e.IsPaid)
                //   .IsRequired()
                //   .HasDefaultValue(false);

                //entity.HasMany(e => e.Terms)
                //    .WithOne(t => t.Partner)
                //    .HasForeignKey(t => t.PartnerId)
                //    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Term>(entity =>
            {
                entity.Property(e => e.TermId)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.PartnerId)
                    .IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TermDescription)
                    .IsRequired()
                    .HasMaxLength(500);

            });
        }

    
    }
}
