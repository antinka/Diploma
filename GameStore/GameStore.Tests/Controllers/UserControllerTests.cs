using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userService;
        private readonly Mock<IRoleService> _roleService;
        private readonly IMapper _mapper;
        private readonly UserController _sut;

        private readonly string _fakeUserName;

        public UserControllerTests()
        {
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _userService = new Mock<IUserService>();
            _roleService = new Mock<IRoleService>();
            _sut = new UserController(_userService.Object, _roleService.Object, _mapper, null);

            _fakeUserName = "test";
        }

        [Fact]
        public void New_ValidUserViewModel_AddNewCalled()
        {
            var fakeUserViewModel = new UserViewModel() { Name = "test", SelectedRolesName = new List<string>()};
            var fakeUserDTO = _mapper.Map<UserDTO>(fakeUserViewModel);

            _userService.Setup(service => service.IsUniqueName(It.IsAny<UserDTO>())).Returns(true);
            _userService.Setup(service => service.AddNew(fakeUserDTO));

            _sut.New(fakeUserViewModel);

            _userService.Verify(s => s.AddNew(It.IsAny<UserDTO>()), Times.Once);
        }

        [Fact]
        public void New_InvalidUserViewModel_ReturnViewResult()
        {
            var fakeUserViewModel = new UserViewModel();
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.New(fakeUserViewModel);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void Get_UserName_GetByNameCalled()
        {
            _userService.Setup(service => service.GetByName(_fakeUserName));

            _sut.Get(_fakeUserName);

            _userService.Verify(s => s.GetByName(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Update_ValidUpdateUser_UpdateCalled()
        {
            var fakeUserViewModel = new UserViewModel() { Name = "test", SelectedRolesName = new List<string>() };
            var fakeUserDTO = _mapper.Map<UserDTO>(fakeUserViewModel);

            _userService.Setup(service => service.IsUniqueName(It.IsAny<UserDTO>())).Returns(true);
            _userService.Setup(service => service.Update(fakeUserDTO));

            _sut.Update(fakeUserViewModel);

            _userService.Verify(s => s.Update(It.IsAny<UserDTO>()), Times.Once);
        }

        [Fact]
        public void Update_InvalidUpdateUser_ReturnViewResult()
        {
            var fakeUserViewModel = new UserViewModel();
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.Update(fakeUserViewModel);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void Remove_UserId_DeleteCalled()
        {
            var fakeUserId = Guid.NewGuid();
            _userService.Setup(service => service.Delete(fakeUserId));

            _sut.Remove(fakeUserId);

            _userService.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GetAll_ReturnedViewResult()
        {
            var fakeUsersDto = new List<UserDTO>()
            {
                new UserDTO() { Name = "test1" },
                new UserDTO() { Name = "test2" },
            };

            _userService.Setup(service => service.GetAll()).Returns(fakeUsersDto);

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
