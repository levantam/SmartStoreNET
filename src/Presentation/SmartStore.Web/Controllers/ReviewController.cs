using SmartStore.Services.Reviews;
using SmartStore.Web.Framework.Controllers;
using SmartStore.Web.Models.Catalog;
using SmartStore.Web.Models.Reviews;
using System.Linq;
using System.Web.Mvc;

namespace SmartStore.Web.Controllers
{
    public class ReviewController : PublicControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly CatalogHelper _helper;

        public ReviewController(IReviewService reviewService, CatalogHelper helper)
        {
            _reviewService = reviewService;
            _helper = helper;
        }

        // GET: Review
        public ActionResult ReviewByProduct(int productId, int page =1, int pageSize = 20, int except = 0)
        {
            var reviews = _reviewService.GetAllReviewByProduct(productId, page, pageSize)
                .Where(t =>t.Id != except)
                .Select(r => new ReviewModel(r) { }).ToList();
            return View(reviews);
        }
        public ActionResult TopReviews()
        {
            var reviews = _reviewService.GetTopReviews(8)
                .Select(r =>  new ReviewModel(r) { }).ToList();
            return View(reviews);
        }

        public ActionResult HighLights()
        {
            var reviews = _reviewService.GetHighLight(3)
                .Select(r => new ReviewModel(r) { }).ToList();
            return View(reviews);
        }

        public  ActionResult ReviewsByReviewer(int id, int page = 1)
        {
            int pageSize = 20;
            var reviews = _reviewService.GetAllReviewByReviewer(id, page, pageSize)
                .Select(r => new ReviewModel(r)).ToList();
            return View(reviews);
        }
        public ActionResult Detail(int id)
        {
            var review = _reviewService.Get(id);
            if (review == null)
            {
                //TODO handle this exception
                throw new System.Exception("Review doesn't exist in the system");
            }
            var productModel = _helper.PrepareProductDetailsPageModel(review.Product, new SmartStore.Services.Catalog.Modelling.ProductVariantQuery());
            return View(new ReviewDetailModel() {
                ReviewModel = new ReviewModel(review),
                ReviewerModel = new ReviewerModel(review.Reviewer),
                ProductModel = productModel
            });
        }
    }
}