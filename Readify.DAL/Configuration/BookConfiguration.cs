using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Readify.DAL.Entities;

namespace Readify.DAL.Configuration
{
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            //builder
            //     .HasOne(b => b.CreatedBy)
            //     .WithMany()
            //     .HasForeignKey(b => b.CreatedById)
            //     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
