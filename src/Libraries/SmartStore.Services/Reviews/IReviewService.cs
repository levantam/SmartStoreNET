using System.Collections;
using System.Collections.Generic;
using SmartStore.Core.Domain.Reviews;

namespace SmartStore.Services.Reviews
{
    public partial interface IReviewService
    {
        IList<Review> GetAllReview();
        IList<Review> GetAllReviewByProduct(int productId, int page = 1, int pageSize = 20);
        IList<Review> GetAllReviewByReviewer(int reviewerId, int page, int pageSize);
        IList<Review> GetHighLight(int size = 3);
        IList<Review> GetTopReviews(int size = 6);

        Review Get(int id);

        void InsertReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(Review review);
    }
}
