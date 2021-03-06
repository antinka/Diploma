﻿using System;
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
    public class PlatformTypeControllerTest
    {
        private readonly Mock<IPlatformTypeService> _platformTypeService;
        private readonly IMapper _mapper;
        private readonly PlatformTypeController _sut;

        private readonly string _fakePlatformTypeName;

        public PlatformTypeControllerTest()
        {
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _platformTypeService = new Mock<IPlatformTypeService>();
            _sut = new PlatformTypeController(_platformTypeService.Object, _mapper, null);

            _fakePlatformTypeName = "test";
        }

        [Fact]
        public void New_ValidPlatformTypeViewModel_AddNewCaleed()
        {
            var fakePlatformTypeViewModel = new PlatformTypeViewModel() { NameEn = "test" };
            var fakePlatformTypeDTO = _mapper.Map<ExtendPlatformTypeDTO>(fakePlatformTypeViewModel);

            _platformTypeService.Setup(service => service.IsUniqueEnName(It.IsAny<ExtendPlatformTypeDTO>()))
                .Returns(true);
            _platformTypeService.Setup(service => service.AddNew(fakePlatformTypeDTO));

            _sut.New(fakePlatformTypeViewModel);

            _platformTypeService.Verify(s => s.AddNew(It.IsAny<ExtendPlatformTypeDTO>()), Times.Once);
        }

        [Fact]
        public void New_InvalidPlatformTypeViewModel_ReturnViewResult()
        {
            var fakePlatformTypeViewModel = new PlatformTypeViewModel() { NameEn = "test" };
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.New(fakePlatformTypeViewModel);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void Get_PlatformTypeName_GetByNameCalled()
        {
            _platformTypeService.Setup(service => service.GetByName(_fakePlatformTypeName));

            _sut.Get(_fakePlatformTypeName);

            _platformTypeService.Verify(s => s.GetByName(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Update_ValidUpdatePlatformType_UpdateCalled()
        {
            var fakePlatformTypeViewModel = new PlatformTypeViewModel() { NameEn = "test" };
            var fakePlatformTypeDTO = _mapper.Map<ExtendPlatformTypeDTO>(fakePlatformTypeViewModel);

            _platformTypeService.Setup(service => service.IsUniqueEnName(It.IsAny<ExtendPlatformTypeDTO>()))
                .Returns(true);
            _platformTypeService.Setup(service => service.Update(fakePlatformTypeDTO));

            _sut.Update(fakePlatformTypeViewModel);

            _platformTypeService.Verify(s => s.Update(It.IsAny<ExtendPlatformTypeDTO>()), Times.Once);
        }

        [Fact]
        public void Update_InvalidUpdatePlatformType_ReturnViewResult()
        {
            var fakePlatformTypeViewModel = new PlatformTypeViewModel();
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.Update(fakePlatformTypeViewModel);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void Update_PlatformTypeName_ReturnedView()
        {
            var res = _sut.Update(_fakePlatformTypeName);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void Remove_PlatformTypeId_DeleteMethodCalled()
        {
            var fakePlatformTypeId = Guid.NewGuid();
            _platformTypeService.Setup(service => service.Delete(fakePlatformTypeId));

            _sut.Remove(fakePlatformTypeId);

            _platformTypeService.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GetAll_ReturnedViewResult()
        {
            var fakePlatformTypesDTO = new List<PlatformTypeDTO>()
            {
                new PlatformTypeDTO() { Name = "test1" }
            };

            _platformTypeService.Setup(service => service.GetAll()).Returns(fakePlatformTypesDTO);

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