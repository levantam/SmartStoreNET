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
            // dumb data
            //var result = new List<Review>()
            //{
            //    // Dave Lee
            //    new Review()
            //    {

            //        Name = "iPhone XR Review: No Need to Panic!",
            //        Cover = "https://img.youtube.com/vi/3mQh7mFgeBc/maxresdefault.jpg",
            //        Thumbnail = "https://img.youtube.com/vi/3mQh7mFgeBc/mqdefault.jpg",
            //        VideoUrl = "https://www.youtube.com/watch?v=3mQh7mFgeBc"
            //    },
            //    new Review()
            //    {
            //        Cover = "https://img.youtube.com/vi/G7LVgdfCNOw/maxresdefault.jpg",
            //        Name = "The Phone of the Year Is Now $500",
            //        Thumbnail = "https://img.youtube.com/vi/G7LVgdfCNOw/mqdefault.jpg",
            //        VideoUrl = "https://www.youtube.com/watch?v=G7LVgdfCNOw"
            //     },
            //    new Review()
            //    {
            //        Cover = "https://img.youtube.com/vi/tSRfYteappo/maxresdefault.jpg",
            //        Name = "OnePlus 6T Review - REAL Changes",
            //        Thumbnail = "https://img.youtube.com/vi/tSRfYteappo/mqdefault.jpg",
            //        VideoUrl = "https://img.youtube.com/vi/tSRfYteappo/mqdefault.jpg"
            //     },
            //    new Review()
            //    {
            //        Cover = "https://img.youtube.com/vi/BUisXF5_TCU/maxresdefault.jpg",
            //        Name = "Pixel 3 XL - The Notch Is Real",
            //        Thumbnail = "https://img.youtube.com/vi/BUisXF5_TCU/mqdefault.jpg",
            //        VideoUrl = "https://www.youtube.com/watch?v=BUisXF5_TCU"
            //     },
            //    new Review()
            //    {
            //        Cover = "https://img.youtube.com/vi/lTTOaXffYZc/maxresdefault.jpg",
            //        Name = "LG V40 - Are 5 Cameras Useful?",
            //        Thumbnail = "https://img.youtube.com/vi/lTTOaXffYZc/mqdefault.jpg",
            //        VideoUrl = "https://www.youtube.com/watch?v=lTTOaXffYZc"
            //     },new Review()
            //    {
            //        Cover = "https://img.youtube.com/vi/qg5HeTGSv3I/maxresdefault.jpg",
            //        Name = "iPhone XS Max - I’m not Switching",
            //        Thumbnail = "https://img.youtube.com/vi/qg5HeTGSv3I/mqdefault.jpg",
            //        VideoUrl = "https://www.youtube.com/watch?v=qg5HeTGSv3I"
            //     },
            //    new Review(){
            //        Cover = "https://img.youtube.com/vi/ujws3_SLMPk/maxresdefault.jpg",
            //        Name = "This Is the OnePlus 6... I Think.",
            //        Thumbnail = "https://img.youtube.com/vi/ujws3_SLMPk/mqdefault.jpg",
            //        VideoUrl = "https://www.youtube.com/watch?v=ujws3_SLMPk"
            //     },
            //    new Review(){
            //        Cover = "https://img.youtube.com/vi/ZADtgM56wvE/maxresdefault.jpg",
            //        Name = "Google Pixel 2 XL + PixelBook - First Impressions!",
            //        Thumbnail = "https://img.youtube.com/vi/ZADtgM56wvE/mqdefault.jpg",
            //        VideoUrl = "https://www.youtube.com/watch?v=ZADtgM56wvE"
            //     },
            //    new Review(){
            //        Cover = "https://img.youtube.com/vi/ZADtgM56wvE/maxresdefault.jpg",
            //        Name = "Google Pixel 2 XL + PixelBook - First Impressions!",
            //        Thumbnail = "https://img.youtube.com/vi/ZADtgM56wvE/mqdefault.jpg",
            //        VideoUrl = "https://www.youtube.com/watch?v=ZADtgM56wvE"
            //     },
            //    new Review(){
            //        Cover = "https://img.youtube.com/vi/3C18M-bsmxY/maxresdefault.jpg",
            //        Name = "ASUS ZenFone 2 Review - Can it beat the 2015 Moto G?",
            //        Thumbnail = "https://img.youtube.com/vi/3C18M-bsmxY/mqdefault.jpg",
            //        VideoUrl = "https://www.youtube.com/watch?v=3C18M-bsmxY"
            //     },
            //    //
            //    //
            //    //Marques Brownlee
            //    new Review(){
            //        Cover = "https://img.youtube.com/vi/ZVoaNzRo0uo/maxresdefault.jpg",
            //        Name = "The Truth About the Pocophone F1!",
            //        Thumbnail = "https://img.youtube.com/vi/ZVoaNzRo0uo/mqdefault.jpg",
            //        VideoUrl = "https://www.youtube.com/watch?v=ZVoaNzRo0uo"
            //     },
            //    new Review(){
            //        Cover = "https://img.youtube.com/vi/Lg9N8XAZ6u4/maxresdefault.jpg",
            //        Name = "Pixel 3 XL Second Impression: Notch City!",
            //        Thumbnail = "https://img.youtube.com/vi/Lg9N8XAZ6u4/mqdefault.jpg",
            //        VideoUrl = "https://www.youtube.com/watch?v=Lg9N8XAZ6u4"
            //     },
            //    new Review(){
            //        Cover = "https://img.youtube.com/vi/GSWOs4_etoE/maxresdefault.jpg",
            //        Name = "iPhone Xs and iPhone Xs Max Impressions!",
            //        Thumbnail = "https://img.youtube.com/vi/GSWOs4_etoE/mqdefault.jpg",
            //        VideoUrl = "https://www.youtube.com/watch?v=GSWOs4_etoE"
            //     },
            //    new Review(){
            //        Cover = "https://img.youtube.com/vi/9AxYQOX5_FM/maxresdefault.jpg",
            //        Name = "Samsung Galaxy Note 9 Impressions: Underrated!",
            //        Thumbnail = "https://img.youtube.com/vi/9AxYQOX5_FM/mqdefault.jpg",
            //        VideoUrl = "https://www.youtube.com/watch?v=9AxYQOX5_FM"
            //     },
            //    new Review(){
            //        Cover = "https://img.youtube.com/vi/ThkMi9fkdEc/maxresdefault.jpg",
            //        Name = "Let's Talk About Smartphone Chins!",
            //        Thumbnail = "https://img.youtube.com/vi/ThkMi9fkdEc/mqdefault.jpg",
            //        VideoUrl = "https://www.youtube.com/watch?v=ThkMi9fkdEc"
            //     },
            //    new Review(){
            //        Cover = "https://img.youtube.com/vi/F0rYqvRk9r8/maxresdefault.jpg",
            //        Name = "OnePlus 6 Impressions!",
            //        Thumbnail = "https://img.youtube.com/vi/F0rYqvRk9r8/mqdefault.jpg",
            //        VideoUrl = "https://www.youtube.com/watch?v=F0rYqvRk9r8"
            //     }
            //};
            //var product = _productRepository.GetById(1);
            //var reviewer = _reviewerRepository.GetById(2);
            //// default properties for all data
            //foreach (var item in result)
            //{
            //    //item.CreatedDate =;
            //    item.Description = "Marc talked about the process of learning a foreign language and the different levels of fluency. He will show that there is a higher realm of language proficiency and explain what it takes to reach this “native” point where the benefits far surpass mere communication skills. Marc’s passion is the study of languages, their manifestation in local dialects, as well as their expression in poetry and folkloric song. He has acquired a near-native proficiency in six languages and their sub-forms and has given various musical performances. This talk was given at a TEDx event using the TED conference format but independently organized by a local community. Learn more at ";
            //    item.IsActive = true;
            //    item.IsDelete = false;
            //    item.IsHighlight = true;
            //    item.IsPremium = true;
            //    item.ShortDescription = "Marc talked about the process of learning a foreign language and the different levels of fluency. He will show that there is a higher realm of language proficiency and explain what it takes to reach this “native” point where the benefits far surpass mere communication skills.";
            //    item.Rating = 5;
            //    item.ShortUrl = "";
            //    //item.UpdatedDate = null;
            //    item.Url = "";
            //    item.Product = product;
            //    item.Reviewer = reviewer;
            //}
            //this._reviewRepository.InsertRange(result);
      
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
