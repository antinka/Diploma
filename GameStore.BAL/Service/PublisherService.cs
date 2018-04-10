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

        public void AddNew(PublisherDTO publisherDTO)
        {
            var publisher = _unitOfWork.Publishers.Get(x => x.Name == publisherDTO.Name);

            if (!publisher.Any())
            {
                publisherDTO.Id = Guid.NewGuid();
                _unitOfWork.Publishers.Create(_mapper.Map<Publisher>(publisherDTO));
                _unitOfWork.Save();

                _log.Info($"{nameof(PublisherService)} - add new publisher{ publisherDTO.Id}");
            }
            else
            {
                throw new EntityNotFound($"{nameof(PublisherService)} - attempt to add new publisher with not unique name");
            }
        }

        public PublisherDTO Get(string companyName)
        {
            var publisher = _unitOfWork.Publishers.Get(x => x.Name == companyName).FirstOrDefault();

            if (publisher == null)
            {
                throw new EntityNotFound($"{nameof(PublisherService)} - publisher with such company name {companyName} did not exist");
            }
            else
            {
                return _mapper.Map<PublisherDTO>(publisher);
            }
        }

        public IEnumerable<PublisherDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<PublisherDTO>>(_unitOfWork.Publishers.GetAll());
        }
    }
}
