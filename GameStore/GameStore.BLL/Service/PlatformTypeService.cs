﻿using AutoMapper;
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
            var platgormType = TakePlatformTypeById(id);

            return _mapper.Map<PlatformTypeDTO>(platgormType);
        }

        public IEnumerable<PlatformTypeDTO> GetAll()
        {
            var platgormTypes = _unitOfWork.PlatformTypes.GetAll();

            return _mapper.Map<IEnumerable<PlatformTypeDTO>>(platgormTypes);
        }

        public bool AddNew(PlatformTypeDTO platformTypeDto)
        {
            if (IsUniqueName(platformTypeDto))
            {
                platformTypeDto.Id = Guid.NewGuid();
                _unitOfWork.PlatformTypes.Create(_mapper.Map<PlatformType>(platformTypeDto));
                _unitOfWork.Save();

                _log.Info($"{nameof(PublisherService)} - add new platformType {platformTypeDto.Id}");

                return true;
            }

            _log.Info($"{nameof(PlatformTypeService)} - attempt to add new platformType with not unique name");

            return false;
        }

        public bool Update(PlatformTypeDTO platformTypeDto)
        {
            if (TakePlatformTypeById(platformTypeDto.Id) != null)
            {
                if (IsUniqueName(platformTypeDto))
                {
                    var platformType = _mapper.Map<PlatformType>(platformTypeDto);

                    _unitOfWork.PlatformTypes.Update(platformType);
                    _unitOfWork.Save();

                    _log.Info($"{nameof(PlatformTypeService)} - update platformType {platformTypeDto.Id}");

                    return true;
                }

                _log.Info($"{nameof(PlatformTypeService)} - attempt to update platformType with not unique name");

                return false;
            }

            return false;
        }

        public void Delete(Guid id)
        {
            if (TakePlatformTypeById(id) != null)
            {
                _unitOfWork.PlatformTypes.Delete(id);
                _unitOfWork.Save();

                _log.Info($"{nameof(PlatformTypeService)} - delete platformType {id}");
            }
        }

        public PlatformTypeDTO GetByName(string name)
        {
            var platformType = _unitOfWork.PlatformTypes.Get(p => p.Name == name).FirstOrDefault();

            if (platformType == null)
            {
                throw new EntityNotFound($"{nameof(PlatformTypeService)} - platformType with such name {name} did not exist");
            }

            return _mapper.Map<PlatformTypeDTO>(platformType);
        }

        private bool IsUniqueName(PlatformTypeDTO platformTypeDTO)
        {
            var platformType = _unitOfWork.PlatformTypes.Get(x => x.Name == platformTypeDTO.Name).FirstOrDefault();

            if (platformType == null || platformTypeDTO.Id == platformType.Id)
                return true;

            return false;
        }

		//Use Get instead of Take. here and everywhere
        private PlatformType TakePlatformTypeById(Guid id)
        {
            var platformType = _unitOfWork.PlatformTypes.GetById(id);

            if (platformType == null)
                throw new EntityNotFound($"{nameof(PlatformTypeService)} - platformType with such id {id} did not exist");

            return platformType;
        }
    }
}
