using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.CustomExeption;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;

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

        public void AddNew(ExtendPublisherDTO publisherDTO)
        {
            publisherDTO.Id = Guid.NewGuid();
            _unitOfWork.Publishers.Create(_mapper.Map<Publisher>(publisherDTO));
            _unitOfWork.Save();

            _log.Info($"{nameof(PublisherService)} - add new publisher{ publisherDTO.Id}");
        }

        public ExtendPublisherDTO GetByName(string companyName)
        {
            var publisher = _unitOfWork.Publishers.Get(x => x.Name == companyName).FirstOrDefault();

            if (publisher == null)
            {
                throw new EntityNotFound($"{nameof(PublisherService)} - publisher with such company name {companyName} did not exist");
            }

            return _mapper.Map<ExtendPublisherDTO>(publisher);
        }

        public IEnumerable<PublisherDTO> GetAll()
        {
            var publishers = _unitOfWork.Publishers.GetAll();

            return _mapper.Map<IEnumerable<PublisherDTO>>(publishers);
        }

        public void Update(ExtendPublisherDTO publisherDTO)
        {
            if (GetPublisherById(publisherDTO.Id) != null)
            {
                var publisher = _mapper.Map<Publisher>(publisherDTO);

                _unitOfWork.Publishers.Update(publisher);
                _unitOfWork.Save();

                _log.Info($"{nameof(PublisherService)} - update publisher {publisherDTO.Id}");
            }
        }

        public void Delete(Guid id)
        {
            if (GetPublisherById(id) != null)
            {
                _unitOfWork.Publishers.Delete(id);
                _unitOfWork.Save();

                _log.Info($"{nameof(PublisherService)} - delete publisher {id}");
            }
        }

        public bool IsUniqueName(ExtendPublisherDTO publisherDTO)
        {
            var publisher = _unitOfWork.Publishers.Get(x => x.Name == publisherDTO.Name).FirstOrDefault();

            if (publisher == null || publisherDTO.Id == publisher.Id)
            {
                return true;
            }

            return false;
        }

        private Publisher GetPublisherById(Guid id)
        {
            var publisher = _unitOfWork.Publishers.GetById(id);

            if (publisher == null)
            {
                throw new EntityNotFound($"{nameof(PublisherService)} - publisher with such id {id} did not exist");
            }

            return publisher;
        }
    }
}
