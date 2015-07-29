using System.Linq;
using Vin.Core.Data.Repositories;
using Vin.Core.Model.Locations;

namespace Vin.Core.Services.Location
{
    public class StateService : IStateService
    {
        private readonly IRepository<State> _stateRepository;

        public StateService(IRepository<State> stateRepository)
        {
            this._stateRepository = stateRepository;
        }

        public IQueryable<State> Table
        {
            get { return _stateRepository.Table; }
        }

        public void Insert(State entity)
        {
            _stateRepository.Insert(entity);
        }

        public void Update(State entity)
        {
            _stateRepository.Update(entity);
        }

        public State GetByID(int id)
        {
            return _stateRepository.GetById(id);
        }
    }
}
