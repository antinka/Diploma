using System;
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
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork uow, IMapper mapper, ILog log)
        {
            _unitOfWork = uow;
            _mapper = mapper;
            _log = log;
        }

        public void AddNew(RoleDTO roleDto)
        {
            roleDto.Id = Guid.NewGuid();
            var newRole = _mapper.Map<Role>(roleDto);

            _unitOfWork.Roles.Create(newRole);
            _unitOfWork.Save();

            _log.Info($"{nameof(RoleService)} - add new role {roleDto.Id}");
        }

        public void Update(RoleDTO roleDto)
        {
            if (GetRoleById(roleDto.Id) != null)
            {
                var role = _mapper.Map<Role>(roleDto);

                _unitOfWork.Roles.Update(role);
                _unitOfWork.Save();

                _log.Info($"{nameof(UserService)} - update role {roleDto.Id}");
            }
        }

        public void Delete(Guid id)
        {
            if (GetRoleById(id) != null)
            {
                _unitOfWork.Roles.Delete(id);
                _unitOfWork.Save();

                _log.Info($"{nameof(RoleService)} - delete role {id}");
            }
        }

        public RoleDTO GetById(Guid id)
        {
            var role = GetRoleById(id);

            return _mapper.Map<RoleDTO>(role);
        }

        public bool IsUniqueName(RoleDTO roleDto)
        {
            var role = _unitOfWork.Games.Get(x => x.Key == roleDto.Name).FirstOrDefault();

            if (role == null)
                return true;

            return false;
        }

        private Role GetRoleById(Guid id)
        {
            var role = _unitOfWork.Roles.GetById(id);

            if (role == null)
                throw new EntityNotFound($"{nameof(RoleService)} - attempt to take not existed role, id {id}");

            return role;
        }
    }
}
