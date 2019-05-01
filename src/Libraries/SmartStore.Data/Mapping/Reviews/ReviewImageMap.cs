using SmartStore.Core.Domain.Reviews;
using System.Data.Entity.ModelConfiguration;

namespace SmartStore.Data.Mapping.Reviews
{
    public partial class ReviewImageMap: EntityTypeConfiguration<ReviewImage>
    {
        public ReviewImageMap()
        {
            this.ToTable("ReviewImage");
        }
    }
}
