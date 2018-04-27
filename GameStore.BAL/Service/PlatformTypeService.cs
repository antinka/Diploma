using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Entities;
using log4net;

namespace GameStore.BLL.Service
{
    public class PlatformTypeService : IPlatformTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILog _log;

        public PlatformTypeService(IUnitOfWork uow, IMapper mapper, ILog log)
        {
            _unitOfWork = uow;
            _mapper = mapper;
            _log = log;
        }

        public PlatformTypeDTO GetById(Guid id)
        {
            var platgormType = _unitOfWork.PlatformTypes.GetById(id);

            if (platgormType == null)
            {
                throw new EntityNotFound($"{nameof(PlatformTypeService)} - platformType with such id {id} did not exist");
            }
			
            return _mapper.Map<PlatformTypeDTO>(platgormType);
        }

        public IEnumerable<PlatformTypeDTO> GetAll()
        {
            var platgormTypes = _unitOfWork.PlatformTypes.GetAll();

            return _mapper.Map<IEnumerable<PlatformTypeDTO>>(platgormTypes);
        }

        public void AddNew(PlatformTypeDTO platformTypeDto)
        {
            var platformType = _unitOfWork.PlatformTypes.Get(x => x.Name == platformTypeDto.Name).FirstOrDefault();

            if (platformType == null)
            {
                platformTypeDto.Id = Guid.NewGuid();
                _unitOfWork.PlatformTypes.Create(_mapper.Map<PlatformType>(platformTypeDto));
                _unitOfWork.Save();

                _log.Info($"{nameof(PublisherService)} - add new platformType {platformTypeDto.Id}");
            }
            else
            {
                throw new NotUniqueParameter($"{nameof(PlatformTypeService)} - attempt to add new platformType with not unique name");
            }
        }
        private PlatformType TakPlatformTypeById(Guid id)
        {
            var platformType = _unitOfWork.PlatformTypes.GetById(id);

            if (platformType == null)
                throw new EntityNotFound($"{nameof(PlatformTypeService)} - platformType with such id {id} did not exist");

            return platformType;
        }

        public void Update(PlatformTypeDTO platformTypeDto)
        {
            TakPlatformTypeById(platformTypeDto.Id);

            var platformType = _mapper.Map<PlatformType>(platformTypeDto);

            _unitOfWork.PlatformTypes.Update(platformType);
            _unitOfWork.Save();

            _log.Info($"{nameof(PlatformTypeService)} - update platformType {platformTypeDto.Id}");
        }

        public void Delete(Guid id)
        {
            TakPlatformTypeById(id);

            _unitOfWork.PlatformTypes.Delete(id);
            _unitOfWork.Save();

            _log.Info($"{nameof(PlatformTypeService)} - delete platformType {id}");
        }

        public PlatformTypeDTO GetByName(string name)
        {
            var platformType = _unitOfWork.PlatformTypes.Get(x => x.Name == name).FirstOrDefault();

            if (platformType == null)
            {
                throw new EntityNotFound($"{nameof(PlatformTypeService)} - platformType with such name {name} did not exist");
            }

            return _mapper.Map<PlatformTypeDTO>(platformType);
        }
    }
}
