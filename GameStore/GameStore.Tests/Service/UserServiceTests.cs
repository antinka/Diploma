using AutoMapper;
using GameStore.BLL.CustomExeption;
using GameStore.BLL.DTO;
using GameStore.BLL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.Web.Infrastructure.Mapper;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GameStore.Tests.Service
{
    public class UserServiceTests
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly UserService _sut;
        private readonly IMapper _mapper;

        private readonly Guid _fakeUserId;
        private readonly string _fakeUserName;
        private readonly User _fakeUser;
        private readonly List<User> _fakeUsers;

        public UserServiceTests()
        {
            _uow = new Mock<IUnitOfWork>();
            var log = new Mock<ILog>();
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _sut = new UserService(_uow.Object, _mapper, log.Object);

            _fakeUserId = Guid.NewGuid();
            _fakeUserName = "name";
            _fakeUser = new User()
            {
                Id = _fakeUserId,
                Name = _fakeUserName
            };
            _fakeUsers = new List<User>()
            {
                _fakeUser
            };
        }

        [Fact]
        public void GetAllUsers_AllUsersReturned()
        {
            _uow.Setup(uow => uow.Users.GetAll()).Returns(_fakeUsers);

            var res = _sut.GetAll();

            Assert.Equal(res.Count(), _fakeUsers.Count);
        }

        [Fact]
        public void GetUserByName_ExistedUserName_RoleReturned()
        {
            _uow.Setup(uow => uow.Users.Get(It.IsAny<Func<User, bool>>())).Returns(_fakeUsers);

            var res = _sut.GetByName(_fakeUserName);

            Assert.True(res.Name == _fakeUserName);
        }

        [Fact]
        public void GetUserByName_NotExistedUserName_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Users.Get(It.IsAny<Func<User, bool>>())).Returns(new List<User>());

            Assert.Throws<EntityNotFound>(() => _sut.GetByName(_fakeUserName));
        }

        [Fact]
        public void AddNewUser_UserWithUniqueName_CreateCalled()
        {
            var fakeUserDTO = new UserDTO() { Id = Guid.NewGuid(), Name = "test", Password = "123"};
            var fakeUser = _mapper.Map<User>(fakeUserDTO);

            _uow.Setup(uow => uow.Roles.Get(It.IsAny<Func<Role, bool>>())).Returns(new List<Role>());
            _uow.Setup(uow => uow.Users.Create(fakeUser));

            _sut.AddNew(fakeUserDTO);

            _uow.Verify(uow => uow.Users.Create(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void UpdateUser_User_UpdateCalled()
        {
            var fakeUserDTO = new UserDTO() { Id = _fakeUserId, Name = "test" };
            var fakeUser = _mapper.Map<User>(fakeUserDTO);

            _uow.Setup(uow => uow.Users.GetById(_fakeUserId)).Returns(fakeUser);
            _uow.Setup(uow => uow.Roles.Get(It.IsAny<Func<Role, bool>>())).Returns(new List<Role>());
            _uow.Setup(uow => uow.Users.Update(fakeUser));

            _sut.Update(fakeUserDTO);

            _uow.Verify(uow => uow.Users.Update(It.IsAny<User>()), Times.AtLeastOnce);
        }

        [Fact]
        public void UpdateUser_NotExistUserName_EntityNotFound()
        {
            _uow.Setup(uow => uow.Users.GetById(_fakeUserId)).Returns(null as User);

            Assert.Throws<EntityNotFound>(() => _sut.Update(new UserDTO()));
        }

        [Fact]
        public void DeleteUser_NotExistedUserName__ExeptionEntityNotFound()
        {
            var notExistUserId = Guid.NewGuid();

            _uow.Setup(uow => uow.Users.GetById(notExistUserId)).Returns(null as User);
            _uow.Setup(uow => uow.Users.Delete(notExistUserId));

            Assert.Throws<EntityNotFound>(() => _sut.Delete(notExistUserId));
        }

        [Fact]
        public void DeleteRole_ExistedUserName__DeleteCalled()
        {
            _uow.Setup(uow => uow.Users.GetById(_fakeUserId)).Returns(_fakeUser);
            _uow.Setup(uow => uow.Users.Delete(_fakeUserId));

            _sut.Delete(_fakeUserId);

            _uow.Verify(uow => uow.Users.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void IsUniqueName_UniqueName_True()
        {
            var fakeUser = new UserDTO() { Id = Guid.NewGuid(), Name = "name" };
            _uow.Setup(uow => uow.Users.Get(It.IsAny<Func<User, bool>>())).Returns(new List<User>());

            var res = _sut.IsUniqueName(fakeUser);

            Assert.True(res);
        }

        [Fact]
        public void IsUniqueName_NotUniqueName_False()
        {
            var fakeUser = new UserDTO() { Id = Guid.NewGuid(), Name = _fakeUserName };
            _uow.Setup(uow => uow.Users.Get(It.IsAny<Func<User, bool>>())).Returns(_fakeUsers);

            var res = _sut.IsUniqueName(fakeUser);

            Assert.False(res);
        }
    }
}
