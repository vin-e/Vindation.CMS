using System.Web.Http;
using Vin.Sample.Business.Marketing;
using Vin.Sample.Data.Model.Marketing;

namespace Vin.Sample.Web.Controllers
{
    public class MarketingController : ApiController
    {
        private readonly INewsletterService _newsletterService;

        public MarketingController(INewsletterService newsletterService)
        {
            this._newsletterService = newsletterService;
        }

        [HttpPost()]
        public bool Newsletter(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            _newsletterService.Insert(new Newsletter() { Email = email, OptIn = true });

            return true;
        }
    }
}
