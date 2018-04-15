using System;
using System.Collections.Generic;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;
using log4net;

namespace GameStore.BLL.Service
{
    public class PlatformTypeService : IPlatformTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly IMapper _mapper;

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
                throw new EntityNotFound($"{nameof(PlatformTypeService)} - platgorm type with such id {id} did not exist");
            }
            else
            {
                return _mapper.Map<PlatformTypeDTO>(platgormType);
            }
        }

        public IEnumerable<PlatformTypeDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<PlatformTypeDTO>>(_unitOfWork.PlatformTypes.GetAll());
        }
    }
}
