using SmartStore.Core.Domain.Reviews;
using SmartStore.Web.Framework.Modelling;
using System;

namespace SmartStore.Web.Models.Reviews
{
    public class ReviewModel: EntityModelBase
    {
        public ReviewModel() { }
        public ReviewModel(Review review)
        {
            if (review != null)
            {
                Id = review.Id;
                Name = review.Name;
                Url = review.Url;
                ShortUrl = review.ShortUrl;
                Cover = review.Cover;
                ShortDescription = review.ShortDescription;
                Description = review.Description;
                IsActive = review.IsActive;
                IsDelete = review.IsDelete;
                IsHighlight = review.IsHighlight;
                IsPremium = review.IsPremium;
                Rating = review.Rating;
                CreatedDate = review.CreatedDate;
                UpdatedDate = review.UpdatedDate;
                VideoUrl = review.VideoUrl;
                Thumbnail = review.Thumbnail;
                Reviewer = new ReviewerModel(review.Reviewer);
                ProductId = review.Product.Id;
            }
        }

        public string Name { get; set; }
        public string Url { get; set; }
        public string ShortUrl { get; set; }
        public string Cover { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; } // full description: both image, text
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool IsHighlight { get; set; }
        public bool IsPremium { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string VideoUrl { get; set; }
        public string Thumbnail { get; set; }
        public ReviewerModel Reviewer { get; set; }

        public int ProductId { get; set; }
    }
}