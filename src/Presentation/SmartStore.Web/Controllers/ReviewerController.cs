using SmartStore.Services.Reviews;
using SmartStore.Web.Framework.Controllers;
using SmartStore.Web.Models.Reviews;
using System.Linq;
using System.Web.Mvc;


namespace SmartStore.Web.Controllers
{
    public class ReviewerController : PublicControllerBase
    {
        private readonly IReviewerService _reviewerService;
        public ReviewerController(
            IReviewerService reviewerService
            )
        {
            _reviewerService = reviewerService;
        }
        // GET: Reviewer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TopReviewers()
        {
            var reviewer = _reviewerService.TopReviewers(1, 6, true)
                .Select(t=> new ReviewerModel(t))
                .ToList();
            return View(reviewer);
        }
        public ActionResult Detail(int id)
        {
            var reviewer = _reviewerService.GetById(id);
            if (reviewer == null)
            {
                throw new System.Exception("Reviewer not found!");
            }
            return View(new ReviewerModel(reviewer));
        }
        public ActionResult Register()
        {
            return View();
        }

    }
}