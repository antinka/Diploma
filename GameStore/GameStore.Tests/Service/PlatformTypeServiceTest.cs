﻿using AutoMapper;
using GameStore.BLL.Exeption;
using GameStore.BLL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.DTO;
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
        private readonly string _fakePlatformTypeName;
        private readonly List<PlatformType> _fakePlatformTypes;

        public PlatformTypeServiceTest()
        {
            _uow = new Mock<IUnitOfWork>();
            var log = new Mock<ILog>();
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _sut = new PlatformTypeService(_uow.Object, _mapper, log.Object);

            _fakePlatformTypeId = Guid.NewGuid();
            _fakePlatformTypeName = "PlatformType1";

            _fakePlatformType = new PlatformType
            {
                Id = _fakePlatformTypeId,
                Name = _fakePlatformTypeName
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

        [Fact]
        public void AddNewPlatformType_PlatformTypeWithUniqueName_Verifiable()
        {
            var fakePlatformTypeDTO = new PlatformTypeDTO() { Id = Guid.NewGuid(), Name = "uniqueName" };
            var fakePlatformType = _mapper.Map<PlatformType>(fakePlatformTypeDTO);

            _uow.Setup(uow => uow.PlatformTypes.Get(It.IsAny<Func<PlatformType, bool>>())).Returns(new List<PlatformType>());
            _uow.Setup(uow => uow.PlatformTypes.Create(fakePlatformType)).Verifiable();

            _sut.AddNew(fakePlatformTypeDTO);

            _uow.Verify(uow => uow.PlatformTypes.Create(It.IsAny<PlatformType>()), Times.Once);
        }

        [Fact]
        public void AddNewPlatformType_PlatformTypeWithoutUniqueName_ReturnedFalseAddNewPlatformType()
        {
            var fakePlatformTypeDTO = _mapper.Map<PlatformTypeDTO>(_fakePlatformType);

            _uow.Setup(uow => uow.PlatformTypes.Get(It.IsAny<Func<PlatformType, bool>>())).Returns(_fakePlatformTypes);

            Assert.False(_sut.AddNew(fakePlatformTypeDTO));
        }

        [Fact]
        public void UpdatePlatformType_PlatformType_PlatformTypeUpdated()
        {
            var fakePlatformTypeDTO = _mapper.Map<PlatformTypeDTO>(_fakePlatformType);

            _uow.Setup(uow => uow.PlatformTypes.GetById(_fakePlatformTypeId)).Returns(_fakePlatformType);

            _uow.Setup(uow => uow.PlatformTypes.Update(_fakePlatformType)).Verifiable();

            _sut.Update(fakePlatformTypeDTO);

            _uow.Verify(uow => uow.PlatformTypes.Update(It.IsAny<PlatformType>()), Times.Once);
        }

        [Fact]
        public void UpdatePlatformType_NotExistPlatformTypeName_EntityNotFound()
        {
            var fakePlatformTypeId = Guid.NewGuid();

            _uow.Setup(uow => uow.PlatformTypes.GetById(fakePlatformTypeId)).Returns(null as PlatformType);

            Assert.Throws<EntityNotFound>(() => _sut.Update(new PlatformTypeDTO()));
        }

        [Fact]
        public void DeletePlatformType_NotExistedPlatformTypeName__ExeptionEntityNotFound()
        {
            var notExistPlatformTypeId = Guid.NewGuid();

            _uow.Setup(uow => uow.PlatformTypes.GetById(notExistPlatformTypeId)).Returns(null as PlatformType);
            _uow.Setup(uow => uow.PlatformTypes.Delete(notExistPlatformTypeId));

            Assert.Throws<EntityNotFound>(() => _sut.Delete(notExistPlatformTypeId));
        }

        [Fact]
        public void DeletePlatformType_ExistedPlatformTypeName__Verifiable()
        {
            _uow.Setup(uow => uow.PlatformTypes.GetById(_fakePlatformTypeId)).Returns(_fakePlatformType);
            _uow.Setup(uow => uow.PlatformTypes.Delete(_fakePlatformTypeId)).Verifiable();

            _sut.Delete(_fakePlatformTypeId);

            _uow.Verify(uow => uow.PlatformTypes.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GetPlatformTypeByName_ExistedPlatformTypeName_PlatformTypeReturned()
        {
            var fakePlatformTypes = new List<PlatformType>()
            {
                _fakePlatformType
            };
            _uow.Setup(uow => uow.PlatformTypes.Get(It.IsAny<Func<PlatformType, bool>>())).Returns(fakePlatformTypes);

            var result = _sut.GetByName(_fakePlatformTypeName);

            Assert.True(result.Name == _fakePlatformTypeName);
        }

        [Fact]
        public void GetPlatformTypeByName_NotExistedPlatformTypeName_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.PlatformTypes.Get(It.IsAny<Func<PlatformType, bool>>())).Returns(new List<PlatformType>());

            Assert.Throws<EntityNotFound>(() => _sut.GetByName(_fakePlatformTypeName));
        }
    }
}