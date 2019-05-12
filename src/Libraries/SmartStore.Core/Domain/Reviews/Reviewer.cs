
using SmartStore.Core.Domain.Customers;
using SmartStore.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace SmartStore.Core.Domain.Reviews
{
    [DataContract]
    public class Reviewer : BaseEntity
    {
        private Customer _user;
        private ICollection<Review> _reviews;

        public Reviewer()
        {
            IsApproved = false;
            Rating = 0;
            CanPost = false;
            IsOwner = false;
            IsCrawlData = false;
        }

        [DataMember]
        public string ChannelName { get; set; }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("getdate()")]
        public DateTime RegisterdDate { get; set; }

        [DataMember]
        public bool IsApproved { get; set; }

        [DataMember]
        public int Rating { get; set; }

        //[DataMember]
        //[DefaultValue(1)]
        //public int LanguageId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string ShortReview { get; set; }

        [DataMember]
        public bool IsCrawlData { get; set; }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastCrawlDate { get; set; }

        [DataMember]
        public string YoutubeId { get; set; }

        [DataMember]
        public bool CanPost { get; set; }

        [DataMember]
        public bool IsOwner { get; set; }

        [DataMember]
        public virtual ICollection<Review> Reviews
        {
            get { return _reviews ?? (_reviews = new HashSet<Review>()); }
            protected set { _reviews = value; }
        }

        [DataMember]
        public virtual Customer User
        {
            get { return _user ?? (_user = new Customer()); }
            protected set { _user = value; }
        }

        [DataMember]
        public virtual Language Language { get; set; }
    }
}
