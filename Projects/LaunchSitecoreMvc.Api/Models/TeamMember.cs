using LaunchSitecoreMvc.Core.Extensions;
using Sitecore.Data.Items;
using System;

namespace LaunchSitecoreMvc.Api.Models
{
    public class TeamMember
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Abstract { get; set; }
        public string JobTitle { get; set; }
        public string Quote { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }

        public TeamMember()
        {

        }

        public TeamMember(Item item)
        {
            if (item != null)
            {
                ID = item.ID.Guid;
                Title = item.Fields["Title"].Value;
                Body = item.Fields["Body"].Value;
                Abstract = item.Fields["Abstract"].Value;
                JobTitle = item.Fields["Job Title"].Value;
                Quote = item.Fields["Quote"].Value;
                Image = item.GetImageUrl("Image");
                Icon = item.GetImageUrl("Icon");
            }
        }
    }
}
