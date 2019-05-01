using SmartStore.Services.Reviews;
using SmartStore.Web.Framework.Controllers;
using SmartStore.Web.Models.Reviews;
using System.Linq;
using System.Web.Mvc;

namespace SmartStore.Web.Controllers
{
    public class ReviewController : PublicControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: Review
        public ActionResult ReviewByProduct(int productId)
        {
            var reviews = _reviewService.GetAllReviewByProduct(productId)
                .Select(r => new ReviewModel() { });
            return View(reviews);
        }
        public ActionResult TopReviews()
        {
            var reviews = _reviewService.GetTopReviews(3)
                .Select(r =>  new ReviewModel() { }).ToList();
            return View(reviews);
        }

        public ActionResult HighLights()
        {
            var reviews = _reviewService.GetHighLight(3)
                .Select(r => new ReviewModel() { }).ToList();
            return View(reviews);
        }
    }
}