using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.CustomExeption;
using GameStore.BLL.DTO;
using GameStore.BLL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.Web.Infrastructure.Mapper;
using log4net;
using Moq;
using Xunit;

namespace GameStore.Tests.Service
{
    public class RoleServiceTest
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly RoleService _sut;
        private readonly IMapper _mapper;

        private readonly Guid _fakeRoleId;
        private readonly string _fakeRoleName;
        private readonly Role _fakeRole;
        private readonly List<Role> _fakeRoles;

        public RoleServiceTest()
        {
            _uow = new Mock<IUnitOfWork>();
            var log = new Mock<ILog>();
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _sut = new RoleService(_uow.Object, _mapper, log.Object);

            _fakeRoleId = Guid.NewGuid();
            _fakeRoleName = "RoleName";

            _fakeRole = new Role
            {
                Id = _fakeRoleId,
                Name = _fakeRoleName
            };

            _fakeRoles = new List<Role>()
            {
                _fakeRole
            };
        }

        [Fact]
        public void GetAllRoles_AllRolesReturned()
        {
            _uow.Setup(uow => uow.Roles.GetAll()).Returns(_fakeRoles);

            var res = _sut.GetAll();

            Assert.Equal(res.Count(), _fakeRoles.Count);
        }

        [Fact]
        public void GetRolesByName_ExistedRoleName_RoleReturned()
        {
            _uow.Setup(uow => uow.Roles.Get(It.IsAny<Func<Role, bool>>())).Returns(_fakeRoles);

            var res = _sut.GetByName(_fakeRoleName);

            Assert.True(res.Name == _fakeRoleName);
        }

        [Fact]
        public void GetRoleByName_NotExistedRoleName_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Roles.Get(It.IsAny<Func<Role, bool>>())).Returns(new List<Role>());

            Assert.Throws<EntityNotFound>(() => _sut.GetByName(_fakeRoleName));
        }

        [Fact]
        public void AddNewRole_RoleWithUniqueName_CreateCalled()
        {
            var fakeRoleDTO = new RoleDTO() { Id = Guid.NewGuid(), Name = "role" };
            var fakeRole = _mapper.Map<Role>(fakeRoleDTO);

            _uow.Setup(uow => uow.Roles.Get(It.IsAny<Func<Role, bool>>())).Returns(new List<Role>());
            _uow.Setup(uow => uow.Roles.Create(fakeRole));

            _sut.AddNew(fakeRoleDTO);

            _uow.Verify(uow => uow.Roles.Create(It.IsAny<Role>()), Times.Once);
        }

        [Fact]
        public void UpdateRole_Role_UpdateCalled()
        {
            var fakeRoleDTO = new RoleDTO() { Id = _fakeRoleId, Name = "role" };
            var fakeRole = _mapper.Map<Role>(fakeRoleDTO);

            _uow.Setup(uow => uow.Roles.GetById(_fakeRoleId)).Returns(fakeRole);

            _uow.Setup(uow => uow.Roles.Update(fakeRole));

            _sut.Update(fakeRoleDTO);

            _uow.Verify(uow => uow.Roles.Update(It.IsAny<Role>()), Times.Once);
        }

        [Fact]
        public void UpdateRole_NotExistRoleName_EntityNotFound()
        {
            _uow.Setup(uow => uow.Roles.GetById(_fakeRoleId)).Returns(null as Role);

            Assert.Throws<EntityNotFound>(() => _sut.Update(new RoleDTO()));
        }

        [Fact]
        public void DeleteRole_NotExistedRoleName__ExeptionEntityNotFound()
        {
            var notExistRoleId = Guid.NewGuid();

            _uow.Setup(uow => uow.Roles.GetById(notExistRoleId)).Returns(null as Role);
            _uow.Setup(uow => uow.Roles.Delete(notExistRoleId));

            Assert.Throws<EntityNotFound>(() => _sut.Delete(notExistRoleId));
        }

        [Fact]
        public void DeleteRole_ExistedRoleName_DeleteCalled()
        {
            _uow.Setup(uow => uow.Roles.GetById(_fakeRoleId)).Returns(_fakeRole);
            _uow.Setup(uow => uow.Roles.Delete(_fakeRoleId));

            _sut.Delete(_fakeRoleId);

            _uow.Verify(uow => uow.Roles.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void IsUniqueName_UniqueName_True()
        {
            var role = new RoleDTO() { Id = Guid.NewGuid(), Name = "name" };
            _uow.Setup(uow => uow.Roles.Get(It.IsAny<Func<Role, bool>>())).Returns(new List<Role>());

            var res = _sut.IsUniqueName(role);

            Assert.True(res);
        }

        [Fact]
        public void IsUniqueName_NotUniqueName_False()
        {
            var role = new RoleDTO() { Id = Guid.NewGuid(), Name = _fakeRoleName };
            _uow.Setup(uow => uow.Roles.Get(It.IsAny<Func<Role, bool>>())).Returns(_fakeRoles);

            var res = _sut.IsUniqueName(role);

            Assert.False(res);
        }
    }
}
