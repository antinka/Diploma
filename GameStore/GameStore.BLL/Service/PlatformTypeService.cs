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
            var platgormType = GetPlatformTypeById(id);

            return _mapper.Map<PlatformTypeDTO>(platgormType);
        }

        public IEnumerable<PlatformTypeDTO> GetAll()
        {
            var platgormTypes = _unitOfWork.PlatformTypes.GetAll();

            return _mapper.Map<IEnumerable<PlatformTypeDTO>>(platgormTypes);
        }

        public void AddNew(ExtendPlatformTypeDTO platformTypeDto)
        {
            platformTypeDto.Id = Guid.NewGuid();
            _unitOfWork.PlatformTypes.Create(_mapper.Map<PlatformType>(platformTypeDto));
            _unitOfWork.Save();

            _log.Info($"{nameof(PublisherService)} - add new platformType {platformTypeDto.Id}");
        }

        public void Update(ExtendPlatformTypeDTO platformTypeDTO)
        {
            if (GetPlatformTypeById(platformTypeDTO.Id) != null)
            {
                var platformType = _mapper.Map<PlatformType>(platformTypeDTO);

                _unitOfWork.PlatformTypes.Update(platformType);
                _unitOfWork.Save();

                _log.Info($"{nameof(PlatformTypeService)} - update platformType {platformTypeDTO.Id}");
            }
        }

        public void Delete(Guid id)
        {
            if (GetPlatformTypeById(id) != null)
            {
                _unitOfWork.PlatformTypes.Delete(id);
                _unitOfWork.Save();

                _log.Info($"{nameof(PlatformTypeService)} - delete platformType {id}");
            }
        }

        public ExtendPlatformTypeDTO GetByName(string name)
        {
            var platformType = _unitOfWork.PlatformTypes.Get(p => p.NameEn == name || p.NameRu == name).FirstOrDefault();

            if (platformType == null)
            {
                throw new EntityNotFound($"{nameof(PlatformTypeService)} - platformType with such name " +
                                         $"{name} did not exist");
            }

            return _mapper.Map<ExtendPlatformTypeDTO>(platformType);
        }

        public bool IsUniqueEnName(ExtendPlatformTypeDTO platformTypeDTO)
        {
            var platformType = _unitOfWork.PlatformTypes.Get(x => x.NameEn == platformTypeDTO.NameEn).FirstOrDefault();

            if (platformType == null || platformTypeDTO.Id == platformType.Id)
            {
                return true;
            }

            return false;
        }

        public bool IsUniqueRuName(ExtendPlatformTypeDTO platformTypeDTO)
        {
            var platformType = _unitOfWork.PlatformTypes.Get(x => x.NameRu == platformTypeDTO.NameRu)
                .FirstOrDefault();

            if (platformType == null || platformTypeDTO.Id == platformType.Id)
            {
                return true;
            }

            return false;
        }

        private PlatformType GetPlatformTypeById(Guid id)
        {
            var platformType = _unitOfWork.PlatformTypes.GetById(id);

            if (platformType == null)
            {
                throw new EntityNotFound(
                    $"{nameof(PlatformTypeService)} - platformType with such id {id} did not exist");
            }

            return platformType;
        }
    }
}
