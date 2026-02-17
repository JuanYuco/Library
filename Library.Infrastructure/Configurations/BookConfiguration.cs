using Library.Domain.Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Year).IsRequired();
            builder.Property(c => c.Gender).IsRequired().HasMaxLength(200);
            builder.Property(c => c.PagesNumber).IsRequired();
            builder.Property(c => c.AuthorId).IsRequired();

            builder.HasOne(c => c.Author)
                .WithMany(v => v.Books)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
