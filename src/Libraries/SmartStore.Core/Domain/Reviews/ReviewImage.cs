using SmartStore.Core.Domain.Media;
using System.Runtime.Serialization;

namespace SmartStore.Core.Domain.Reviews
{
    [DataContract]
    public class ReviewImage: BaseEntity
    {
		[DataMember]
        public int DisplayOrder { get; set; }

        [DataMember]
        public virtual Picture Picture { get; set; }

        [DataMember]
        public virtual Review Review { get; set; }
    }
}
