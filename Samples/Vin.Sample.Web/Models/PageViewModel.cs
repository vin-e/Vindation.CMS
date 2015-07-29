using System;
using Vin.Sample.Data.Model.Medias;
using Vin.Sample.Data.Model.Pages;

namespace Vin.Sample.Web.Models
{
    public class PageViewModel
    {
        public PageViewModel() { }
        public PageViewModel(Page page)
        {
            ID = page.ID;
            Title = page.Title;
            HtmlBody = page.HtmlBody;
            TextBody = page.TextBody;
            HeaderImage = page.HeaderImage != null ? page.HeaderImage.Url : string.Empty;
            CreatedDate = page.CreatedDate;
            UpdatedDate = page.UpdatedDate;
        }

        public int ID { get; set; }

        public string Title { get; set; }

        public string HtmlBody { get; set; }

        public string TextBody { get; set; }

        public string HeaderImage { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}