using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Readify.DAL.Entities;

namespace Readify.DAL.Configuration
{
    internal class ReturnRequestConfiguration : IEntityTypeConfiguration<ReturnRequest>
    {
        public void Configure(EntityTypeBuilder<ReturnRequest> builder)
        {
            builder
            .HasOne(rr => rr.BorrowedBook)
            .WithMany(bb => bb.ReturnRequests)
            .HasForeignKey(rr => rr.BorrowedBookId)
            .OnDelete(DeleteBehavior.Restrict);


            builder.Property(rr => rr.CreatedAt)
                    .IsRequired();

            builder.Property(rr => rr.ReturnDate)
                .IsRequired();

            builder.Property(rr => rr.BorrowedBookId)
                .IsRequired();
        }
    }
}
