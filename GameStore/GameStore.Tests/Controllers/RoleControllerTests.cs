using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Web.Controllers;
using GameStore.Web.Infrastructure.Mapper;
using GameStore.Web.ViewModels;
using Moq;
using Xunit;

namespace GameStore.Tests.Controllers
{
    public class RoleControllerTests
    {
        private readonly Mock<IRoleService> _roleService;
        private readonly IMapper _mapper;
        private readonly RoleController _sut;

        private readonly string _fakeRoleName;

        public RoleControllerTests()
        {
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _roleService = new Mock<IRoleService>();
            _sut = new RoleController(_roleService.Object, _mapper, null);

            _fakeRoleName = "test";
        }

        [Fact]
        public void New_ValidRoleViewModel_AddNewCalled()
        {
            var fakeRoleViewModel = new RoleViewModel() { Name = "test" };
            var fakeRoleDTO = _mapper.Map<RoleDTO>(fakeRoleViewModel);

            _roleService.Setup(service => service.IsUniqueName(It.IsAny<RoleDTO>())).Returns(true);
            _roleService.Setup(service => service.AddNew(fakeRoleDTO));

            _sut.New(fakeRoleViewModel);

            _roleService.Verify(s => s.AddNew(It.IsAny<RoleDTO>()), Times.Once);
        }

        [Fact]
        public void New_InvalidRoleViewModel_ReturnViewResult()
        {
            var fakeRoleViewModel = new RoleViewModel();
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.New(fakeRoleViewModel);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void Get_RoleName_GetByNameCalled()
        {
            _roleService.Setup(service => service.GetByName(_fakeRoleName));

            _sut.Get(_fakeRoleName);

            _roleService.Verify(s => s.GetByName(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Update_ValidUpdateRole_UpdateCalled()
        {
            var fakeRoleViewModel = new RoleViewModel() { Name = "test" };
            var fakeRoleDTO = _mapper.Map<RoleDTO>(fakeRoleViewModel);

            _roleService.Setup(service => service.IsUniqueName(It.IsAny<RoleDTO>())).Returns(true);
            _roleService.Setup(service => service.Update(fakeRoleDTO));

            _sut.Update(fakeRoleViewModel);

            _roleService.Verify(s => s.Update(It.IsAny<RoleDTO>()), Times.Once);
        }

        [Fact]
        public void Update_InvalidUpdateRole_ReturnViewResult()
        {
            var fakeRoleViewModel = new RoleViewModel();
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.Update(fakeRoleViewModel);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void Update_RoleName_ReturnView()
        {
            var res = _sut.Update(_fakeRoleName);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void Remove_RoleId_DeleteCalled()
        {
            var fakeRoleId = Guid.NewGuid();
            _roleService.Setup(service => service.Delete(fakeRoleId));

            _sut.Remove(fakeRoleId);

            _roleService.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GetAll_ReturnedViewResult()
        {
            var fakeRolessDto = new List<RoleDTO>()
            {
                new RoleDTO() { Name = "test1" },
                new RoleDTO() { Name = "test2" },
            };

            _roleService.Setup(service => service.GetAll()).Returns(fakeRolessDto);

            var res = _sut.GetAll();

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void New_ReturnViewResult()
        {
            var res = _sut.New();

            Assert.IsType<ViewResult>(res);
        }
    }
}
