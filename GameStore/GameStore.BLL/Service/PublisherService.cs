using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using AutoMapper;
using GameStore.DAL.Interfaces;
using log4net;
using GameStore.BLL.Exeption;
using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.BLL.Service
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly IMapper _mapper;

        public PublisherService(IUnitOfWork uow, IMapper mapper, ILog log)
        {
            _unitOfWork = uow;
            _mapper = mapper;
            _log = log;
        }

        public bool AddNew(PublisherDTO publisherDTO)
        {
            var publisher = _unitOfWork.Publishers.Get(x => x.Name == publisherDTO.Name).FirstOrDefault();

            if (publisher == null)
            {
                publisherDTO.Id = Guid.NewGuid();
                _unitOfWork.Publishers.Create(_mapper.Map<Publisher>(publisherDTO));
                _unitOfWork.Save();

                _log.Info($"{nameof(PublisherService)} - add new publisher{ publisherDTO.Id}");

                return true;
            }
            else
            {
                _log.Info($"{nameof(PublisherService)} - attempt to add new publisher with not unique name");

                return false;
            }
        }

        public PublisherDTO GetByName(string companyName)
        {
            var publisher = _unitOfWork.Publishers.Get(x => x.Name == companyName).FirstOrDefault();

            if (publisher == null)
            {
                throw new EntityNotFound($"{nameof(PublisherService)} - publisher with such company name {companyName} did not exist");
            }
			
            return _mapper.Map<PublisherDTO>(publisher);
        }

        public IEnumerable<PublisherDTO> GetAll()
        {
            var publishers = _unitOfWork.Publishers.GetAll();

            return _mapper.Map<IEnumerable<PublisherDTO>>(publishers);
        }

        private Publisher TakePublisherById(Guid id)
        {
            var publisher = _unitOfWork.Publishers.GetById(id);

            if (publisher == null)
                throw new EntityNotFound($"{nameof(PublisherService)} - publisher with such id {id} did not exist");

            return publisher;
        }

        public void Update(PublisherDTO publisherDTO)
        {
            TakePublisherById(publisherDTO.Id);

            var publisher = _mapper.Map<Publisher>(publisherDTO);

            _unitOfWork.Publishers.Update(publisher);
            _unitOfWork.Save();

            _log.Info($"{nameof(PublisherService)} - update publisher {publisherDTO.Id}");
        }

        public void Delete(Guid id)
        {
            TakePublisherById(id);

            _unitOfWork.Publishers.Delete(id);
            _unitOfWork.Save();

            _log.Info($"{nameof(PublisherService)} - delete publisher {id}");
        }
    }
}
