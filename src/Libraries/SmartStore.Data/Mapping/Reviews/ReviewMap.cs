using SmartStore.Core.Domain.Reviews;
using System;
using System.Data.Entity.ModelConfiguration;

namespace SmartStore.Data.Mapping.Reviews
{
    public class ReviewMap: EntityTypeConfiguration<Review>
    {
        public ReviewMap()
        {
            this.ToTable("Review");
            this.HasKey(t => t.Id);

            this.Property(t => t.Name).HasMaxLength(200);
            this.Property(t => t.Url).HasMaxLength(400);
            this.Property(t => t.ShortUrl).HasMaxLength(100);
            this.Property(t => t.Cover).HasMaxLength(100);
            this.Property(t => t.ShortDescription).HasMaxLength(500);
            this.Property(t => t.Description);
            this.Property(t => t.IsActive);
            this.Property(t => t.IsDelete);
            this.Property(t => t.IsHighlight);
            this.Property(t => t.IsPremium);
            this.Property(t => t.Rating);
            this.Property(t => t.CreatedDate);
            this.Property(t => t.UpdatedDate);
            this.Property(t => t.VideoUrl);
            this.Property(t => t.Thumbnail).HasMaxLength(400);

            // References
            //  this.HasOne(t => t.Creator).WithMany();// .HasForeignKey(t => t.ReviewerId);
            // this.HasOne(t => t.Category).WithMany();// .HasForeignKey(t => t.CategoryId);

            //Rev-Ratings
            //this.HasMany(t => t.Ratings).WithOne(t => t.Review);
            //this.HasMany(t => t.Likes).WithOne(t => t.Review);
            //this.HasMany(t => t.Views).WithOne(t => t.Review);
            //this.HasMany(t => t.Images).WithOne(t => t.Review);
        }
    }
}
