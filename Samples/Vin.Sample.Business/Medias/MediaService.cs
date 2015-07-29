using System.Linq;
using Vin.Core.Data.Repositories;
using Vin.Sample.Data.Model.Medias;

namespace Vin.Sample.Business.Medias
{
    public partial class MediaService : IMediaService
    {
        private IRepository<Media> _mediaRepository;

        public MediaService(IRepository<Media> mediaRepository)
        {
            this._mediaRepository = mediaRepository;
        }

        public IQueryable<Media> Table
        {
            get { return _mediaRepository.Table; }
        }

        public void Insert(Media entity)
        {
            _mediaRepository.Insert(entity);
        }

        public void Update(Media entity)
        {
            _mediaRepository.Update(entity);
        }

        public Media GetByID(int id)
        {
            return _mediaRepository.GetById(id);
        }
    }
}
