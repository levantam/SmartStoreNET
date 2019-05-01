
using SmartStore.Core.Domain.Customers;
using System;
using System.Collections.Generic;
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
        }

        [DataMember]
        public string ChannelName { get; set; }

        [DataMember]
        public DateTime RegisterdDate { get; set; }

        [DataMember]
        public bool IsApproved { get; set; }

        [DataMember]
        public int Rating { get; set; }

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
    }
}
