using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartStore.Core.Data;
using SmartStore.Core.Domain.Reviews;
using SmartStore.Core.Domain.Security;
using SmartStore.Core.Domain.Stores;
using SmartStore.Services.Stores;

namespace SmartStore.Services.Reviews
{
    public partial class ReviewService : IReviewService
    {
        private readonly ICommonServices _services;
        private readonly IRepository<Review> _reviewRepository;
        private readonly IRepository<StoreMapping> _storeMappingRepository;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IRepository<AclRecord> _aclRepository;

        public DbQuerySettings QuerySettings { get; set; }

        public ReviewService(
            ICommonServices services,
            IRepository<Review> reviewRepository,
            IRepository<StoreMapping> storeMappingRepository,
            IStoreMappingService storeMappingService,
            IRepository<AclRecord> aclRepository)
        {
            _services = services;
            _reviewRepository = reviewRepository;
            _storeMappingRepository = storeMappingRepository;
            _storeMappingService = storeMappingService;
            _aclRepository = aclRepository;

            this.QuerySettings = DbQuerySettings.Default;
        }

        public IList<Review> GetAllReview()
        {
            return this._reviewRepository
                .Table.Where(t => FilterByActive(t))
                .OrderByDescending(t => t.Id)
                .ToList();
        }

        public IList<Review> GetAllReviewByProduct(int productId)
        {
            return this._reviewRepository
                .Table
                .Where(t => FilterByActive(t) && t.Product.Id == productId)
                .ToList();
        }
        public Review Get(int id)
        {
            return this._reviewRepository.GetById(id);
        }

        public IList<Review> GetHighLight(int size = 3)
        {
            return this._reviewRepository.Table
                .Where(r => FilterByActive(r) && r.IsHighlight == true)
                .OrderByDescending(t => t.Id)
                .Take(size)
                .ToList();
        }

        public IList<Review> GetTopReviews(int size = 6)
        {
            return this._reviewRepository.Table
                .Where(r => r.IsActive == true && r.IsDelete == false)
                .OrderByDescending(t => t.Id)
                .Take(size)
                .ToList();
        }

        #region Private Methods
        private bool FilterByActive(Review review)
        {
            return review.IsActive == true && review.IsDelete == false;
        }

        public void InsertReview(Review review)
        {
            Guard.NotNull(review, nameof(review));
            _reviewRepository.Insert(review);
        }

        public void UpdateReview(Review review)
        {
            Guard.NotNull(review, nameof(review));

            _reviewRepository.Update(review);
        }

        public void DeleteReview(Review review)
        {
            Guard.NotNull(review, nameof(review));
            _reviewRepository.Delete(review);
        }
        #endregion
    }
}
