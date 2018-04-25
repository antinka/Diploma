using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace GameStore.BLL.Service
{
    public class PlatformTypeService : IPlatformTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlatformTypeService(IUnitOfWork uow, IMapper mapper)
        {
            _unitOfWork = uow;
            _mapper = mapper;
        }

        public PlatformTypeDTO GetById(Guid id)
        {
            var platgormType = _unitOfWork.PlatformTypes.GetById(id);

            if (platgormType == null)
            {
                throw new EntityNotFound($"{nameof(PlatformTypeService)} - platgorm type with such id {id} did not exist");
            }
			
            return _mapper.Map<PlatformTypeDTO>(platgormType);
        }

        public IEnumerable<PlatformTypeDTO> GetAll()
        {
            var platgormTypes = _unitOfWork.PlatformTypes.GetAll();

            return _mapper.Map<IEnumerable<PlatformTypeDTO>>(platgormTypes);
        }
    }
}
