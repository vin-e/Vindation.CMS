using System.Linq;
using Vin.Core.Web;
using Vin.Sample.Data.Model.Pages;

namespace Vin.Sample.Business.Pages
{
    public partial interface IPageService
    {
        IQueryable<Page> Table { get; }
        void Insert(Page entity);
        void Update(Page entity);
        Page GetByID(int id);

        MetaInfo GetMetaData(Page entity);
    }
}
