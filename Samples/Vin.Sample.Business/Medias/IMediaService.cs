using System.Linq;
using Vin.Sample.Data.Model.Medias;

namespace Vin.Sample.Business.Medias
{
    public partial interface IMediaService
    {
        IQueryable<Media> Table { get; }
        void Insert(Media entity);
        void Update(Media entity);
        Media GetByID(int id);
    }
}
