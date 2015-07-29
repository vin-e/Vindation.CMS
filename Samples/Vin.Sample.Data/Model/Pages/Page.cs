using System.ComponentModel.DataAnnotations;
using Vin.Core.Model;
using Vin.Sample.Data.Model.Medias;

namespace Vin.Sample.Data.Model.Pages
{
    public class Page : BaseTenantIdEntity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        [MaxLength(100)]
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the body with html tags
        /// </summary>
        public virtual string HtmlBody { get; set; }

        /// <summary>
        /// Gets or sets the body without html tags
        /// </summary>
        public virtual string TextBody { get; set; }

        /// <summary>
        /// Gets or sets if the page is published
        /// </summary>
        public virtual bool IsPublished { get; set; }
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
        /// Gets or sets the Parent Page
        /// </summary>
        public virtual Page ParentPage { get; set; }

        #endregion
    }
}
