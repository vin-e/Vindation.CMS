using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vin.Core.Model;

namespace Vin.Sample.Data.Model.Posts
{
    public class Category : BaseTenantIdEntity
    {
        /// <summary>
        /// Gets or sets the name of the category
        /// </summary>
        [MaxLength(255)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets a collection of posts for the category
        /// </summary>
        public virtual ICollection<Post> Posts { get; set; }
    }
}
