using System.Linq;
using Vin.Sample.Data.Model.Menus;

namespace Vin.Sample.Business.Menus
{
    public partial interface IMenuService
    {
        IQueryable<Menu> Table { get; }
        void Insert(Menu entity);
        void Update(Menu entity);
        Menu GetByID(int id);

        void Insert(MenuItem entity);
        void Update(MenuItem entity);
    }
}
