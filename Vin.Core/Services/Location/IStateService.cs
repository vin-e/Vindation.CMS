using System.Linq;
using Vin.Core.Model.Locations;

namespace Vin.Core.Services.Location
{
    public interface IStateService
    {
        IQueryable<State> Table { get; }
        void Insert(State entity);
        void Update(State entity);
        State GetByID(int id);
    }
}
