using SmartStore.Web.Framework.Modelling;
using SmartStore.Web.Models.Catalog;
using SmartStore.Web.Models.Reviews;

namespace SmartStore.Web.Models.Reviews
{
    public class ReviewDetailModel: EntityModelBase
    {
        public ReviewModel ReviewModel { get; set; }
        public ReviewerModel ReviewerModel { get; set; }
        public ProductDetailsModel ProductModel { get; set; }

        public ReviewDetailModel() { }
    }
}