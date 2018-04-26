using AutoMapper;
using GameStore.BLL.Exeption;
using GameStore.BLL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Infrastructure.Mapper;
using Xunit;

namespace GameStore.Tests.Service
{
    public class PlatformTypeServiceTest
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly PlatformTypeService _sut;
        private readonly IMapper _mapper;

        private readonly PlatformType _fakePlatformType;
        private readonly Guid _fakePlatformTypeId;
        private readonly List<PlatformType> _fakePlatformTypes;

        public PlatformTypeServiceTest()
        {
            _uow = new Mock<IUnitOfWork>();
            var log = new Mock<ILog>();
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _sut = new PlatformTypeService(_uow.Object, _mapper);

            _fakePlatformTypeId = Guid.NewGuid();

            _fakePlatformType = new PlatformType
            {
                Id = _fakePlatformTypeId,
                Name = "PlatformType1"
            };

            _fakePlatformTypes = new List<PlatformType>()
            {
                _fakePlatformType,

                new PlatformType() { Id = new Guid() }
            };
        }

        [Fact]
        public void GetAllPlatformTypes_AllPlatformTypesReturned()
        {
            _uow.Setup(uow => uow.PlatformTypes.GetAll()).Returns(_fakePlatformTypes);

            var resultPlatformType = _sut.GetAll();

            Assert.Equal(resultPlatformType.Count(), _fakePlatformTypes.Count);
        }

        [Fact]
        public void GetPlatformTypeById_ExistedPlatformTypeId_GameReturned()
        {
            _uow.Setup(uow => uow.PlatformTypes.GetById(_fakePlatformTypeId)).Returns(_fakePlatformType);

            var resultPlatformType = _sut.GetById(_fakePlatformTypeId);

            Assert.True(resultPlatformType.Id == _fakePlatformTypeId);
        }

        [Fact]
        public void GetPlatformTypeById_NotExistedPlatformTypeId_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.PlatformTypes.GetById(_fakePlatformTypeId)).Returns(null as PlatformType);

            Assert.Throws<EntityNotFound>(() => _sut.GetById(_fakePlatformTypeId));
        }
    }
}
