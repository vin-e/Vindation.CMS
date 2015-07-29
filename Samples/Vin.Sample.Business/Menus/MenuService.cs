using System.Linq;
using Vin.Core.Data.Repositories;
using Vin.Sample.Data.Model.Menus;

namespace Vin.Sample.Business.Menus
{
    public partial class MenuService : IMenuService
    {
        private readonly IRepository<Menu> _menuRepository;
        private readonly IRepository<MenuItem> _menuItemRepository;

        public MenuService(IRepository<Menu> menuRepository, IRepository<MenuItem> menuItemRepository)
        {
            this._menuRepository = menuRepository;
            this._menuItemRepository = menuItemRepository;
        }

        #region Menu
        public IQueryable<Menu> Table
        {
            get { return _menuRepository.Table; }
        }

        public void Insert(Menu entity)
        {
            _menuRepository.Insert(entity);
        }

        public void Update(Menu entity)
        {
            _menuRepository.Update(entity);
        }

        public Menu GetByID(int id)
        {
            return _menuRepository.GetById(id);
        }
        #endregion

        #region MenuItems
        public void Insert(MenuItem entity)
        {
            _menuItemRepository.Insert(entity);
        }

        public void Update(MenuItem entity)
        {
            _menuItemRepository.Update(entity);
        }
        #endregion
    }
}
