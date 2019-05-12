using SmartStore.Core.Domain.Reviews;
using System.Data.Entity.ModelConfiguration;

namespace SmartStore.Data.Mapping.Reviews
{
    public partial class ReviewerMap: EntityTypeConfiguration<Reviewer>
    {
        public ReviewerMap()
        {
            this.ToTable("Reviewer");
            //this.HasRequired(t => t.Language)
            //    .WithMany()
            //    .HasForeignKey(t => t.LanguageId)
            //    .WillCascadeOnDelete(true);
        }
    }
}
