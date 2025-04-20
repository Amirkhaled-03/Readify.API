using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Readify.DAL.Entities;

namespace Readify.DAL.Configuration
{
    public class BorrowRequestConfiguration : IEntityTypeConfiguration<BorrowRequest>
    {
        public void Configure(EntityTypeBuilder<BorrowRequest> builder)
        {
            builder
                .HasOne(br => br.User)
                .WithMany(u => u.BorrowRequests)
                .HasForeignKey(br => br.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(br => br.Book)
                .WithMany(b => b.BorrowRequests)
                .HasForeignKey(br => br.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(br => br.ApprovedBy)
                .WithMany()
                .HasForeignKey(br => br.ApprovedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(br => br.RequestedAt)
                .IsRequired();
        }
    }
}
