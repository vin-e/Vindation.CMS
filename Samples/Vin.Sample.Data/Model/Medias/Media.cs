using System.ComponentModel.DataAnnotations;
using Vin.Core.Model;

namespace Vin.Sample.Data.Model.Medias
{
    public class Media : BaseTenantIdEntity
    {
        /// <summary>
        /// Gets or sets the Name of the media file
        /// </summary>
        [MaxLength(255)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the url string of the media file
        /// </summary>
        [MaxLength(500)]
        public virtual string Url { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail url string of the media file
        /// </summary>
        [MaxLength(500)]
        public virtual string ThumbnailUrl { get; set; }

        /// <summary>
        /// Gets or sets the media type value
        /// </summary>
        public virtual MediaTypeEnum MediaType { get; set; }
    }
}
