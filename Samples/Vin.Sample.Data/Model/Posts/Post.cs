using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vin.Core.Model;
using Vin.Sample.Data.Model.Medias;

namespace Vin.Sample.Data.Model.Posts
{
    public class Post : BaseTenantIdEntity
    {
        #region Properties
        
        /// <summary>
        /// Gets or sets the title
        /// </summary>
        [MaxLength(100)]
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the body with Html tags
        /// </summary>
        public virtual string HtmlBody { get; set; }

        /// <summary>
        /// Gets or sets the body without Html tags
        /// </summary>
        public virtual string TextBody { get; set; }

        /// <summary>
        /// Gets or sets if the post is published
        /// </summary>
        public virtual bool IsPublished { get; set; }

        /// <summary>
        /// Gets or sets the blog post start date and time
        /// </summary>
        public virtual DateTime? PublishDate { get; set; }

        #endregion

        #region Navigation

        /// <summary>
        /// Gets or sets the header image based on a media file
        /// </summary>
        public virtual Media HeaderImage { get; set; }

        /// <summary>
        /// Gets or sets the meta data and permalink information
        /// </summary>
        public virtual MetaData Meta { get; set; }

        /// <summary>
        /// Gets or sets the categories for the post
        /// </summary>
        public virtual ICollection<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the tags for the post
        /// </summary>
        public virtual ICollection<Tag> Tags { get; set; }

        #endregion
    }
}
