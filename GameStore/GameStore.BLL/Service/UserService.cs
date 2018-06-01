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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper, ILog log)
        {
            _unitOfWork = uow;
            _mapper = mapper;
            _log = log;
        }

        public void AddNew(UserDTO userDto)
        {
            userDto.Id = Guid.NewGuid();
            var newUser = _mapper.Map<User>(userDto);

            _unitOfWork.Users.Create(newUser);
            _unitOfWork.Save();

            _log.Info($"{nameof(UserService)} - add new user {userDto.Id}");
        }

        public void Update(UserDTO userDto)
        {
            if (GetUserById(userDto.Id) != null)
            {
                var user = _mapper.Map<User>(userDto);

                _unitOfWork.Users.Update(user);
                _unitOfWork.Save();

                _log.Info($"{nameof(UserService)} - update user {userDto.Id}");
            }
        }

        public void Delete(Guid id)
        {
            if (GetUserById(id) != null)
            {
                _unitOfWork.Users.Delete(id);
                _unitOfWork.Save();

                _log.Info($"{nameof(UserService)} - delete user {id}");
            }
        }

        public UserDTO GetById(Guid id)
        {
            var user = GetUserById(id);

            return _mapper.Map<UserDTO>(user);
        }

        public UserDTO GetByName(string name)
        {
            var user = _unitOfWork.Users.Get(x => x.Name == name).FirstOrDefault();

            if (user == null)
                throw new EntityNotFound($"{nameof(UserService)} - user with such name {name} did not exist");

            return _mapper.Map<UserDTO>(user);
        }

        public UserDTO Login(string name, string password)
        {
            var user = _unitOfWork.Users.Get(x => x.Name == name).FirstOrDefault();

            if (user == null)
                return null;

            var encryptedPassword = password.GetHashCode().ToString();

            if (user.Password == encryptedPassword)
            {
                return _mapper.Map<UserDTO>(user);
            }

            return null;
        }

        public bool IsUniqueName(UserDTO userDto)
        {
            var user = _unitOfWork.Users.Get(x => x.Name == userDto.Name).FirstOrDefault();

            if (user == null)
                return true;

            return false;
        }

        private User GetUserById(Guid id)
        {
            var user = _unitOfWork.Users.GetById(id);

            if (user == null)
                throw new EntityNotFound($"{nameof(UserService)} - attempt to take not existed user, id {id}");

            return user;
        }
    }
}
