using SmartStore.Core.Domain.Reviews;
using SmartStore.Web.Framework.Modelling;
using System;

namespace SmartStore.Web.Models.Reviews
{
    public class ReviewerModel: EntityModelBase
    {
        public ReviewerModel() { }
        public ReviewerModel(Reviewer reviewer)
        {
            ChannelUrl = "vatvostudio";
            Id = reviewer.Id;
            UserID = reviewer.User.Id;
            ChannelName = reviewer.ChannelName;
            RegisterdDate = reviewer.RegisterdDate;
            Rating = reviewer.Rating;
            // TODO edit this info :)
            Description = "Kênh youtube công nghệ cá nhân chia sẻ các video hay về Game, ứng dụng trên hệ điều hành di động. Bên cạnh đó còn có các video review, trên tay sản phẩm vv.. mong sẽ giúp chia sẻ được nhiều điều hay tới cộng đồng công nghệ :3";
            Avatar = "https://yt3.ggpht.com/a/AGF-l7_9UT5ZRkm9nDBu2imT3fiCMgQbQ6-0JCZEYg=s288-mo-c-c0xffffffff-rj-k-no";
        }

        public int UserID { get; set; }
        public string ChannelName { get; set; }
        public string ChannelUrl { get; set; }
        public DateTime RegisterdDate { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
    }
}