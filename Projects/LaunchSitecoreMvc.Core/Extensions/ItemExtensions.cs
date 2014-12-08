using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;

namespace LaunchSitecoreMvc.Core.Extensions
{
    public static class ItemExtensions
    {
        /// <summary>
        /// An Item extension method that gets image URL.
        /// </summary>
        /// <param name="item">The item to act on.</param>
        /// <param name="field">The field.</param>
        /// <returns>
        /// The image URL.
        /// </returns>
        public static string GetImageUrl(this Item item, string field)
        {
            string imageURL = string.Empty;
            ImageField imageField = item.Fields[field];

            if (imageField != null && imageField.MediaItem != null)
            {
                var image = new MediaItem(imageField.MediaItem);
                imageURL = Sitecore.StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(image));
            }

            return imageURL;
        }
    }
}
