using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartStore.Core.Data;
using System.Data.Entity;
using SmartStore.Core.Domain.Catalog;
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
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Reviewer> _reviewerRepository;
        private readonly IRepository<StoreMapping> _storeMappingRepository;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IRepository<AclRecord> _aclRepository;

        public DbQuerySettings QuerySettings { get; set; }

        public ReviewService(
            ICommonServices services,
            IRepository<Review> reviewRepository,
            IRepository<Product> productRepository,
            IRepository<Reviewer> reviewerRepository,
            IRepository<StoreMapping> storeMappingRepository,
            IStoreMappingService storeMappingService,
            IRepository<AclRecord> aclRepository)
        {
            _services = services;
            _reviewRepository = reviewRepository;
            _reviewerRepository = reviewerRepository;
            _productRepository = productRepository;
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

        public IList<Review> GetAllReviewByProduct(int productId, int page = 1, int pageSize = 20)
        {
            return this._reviewRepository
                .Table
                .Where(t => t.IsActive == true && t.IsDelete == false && t.Product.Id == productId)
                .OrderByDescending(t => new Guid())
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
        public IList<Review> GetAllReviewByReviewer(int reviewerId, int page=1, int pageSize= 20)
        {
            return this._reviewRepository
                .Table
                .Where(t => t.IsActive == true && t.IsDelete == false && t.Reviewer.Id == reviewerId)
                .OrderByDescending(r => r.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
        public Review Get(int id)
        {
            var review = this._reviewRepository.Table.Where(t => t.Id == id)
                .Include(t => t.Reviewer)
                .Include(t=> t.Product)
                .FirstOrDefault();
            if (review != null)
            {
                
            }
            return review;
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
            return this._reviewRepository
                .Table
                .Where(r => r.IsActive == true && r.IsDelete == false)
                .OrderByDescending(t => t.Id)
                .Skip(0)
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
