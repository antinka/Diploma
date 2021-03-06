﻿using System;
using AutoMapper;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Mongo;
using GameStore.DAL.Mongo.MongoEntities;
using GameStore.DAL.Repositories;

namespace GameStore.DAL.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _context;

        private readonly Lazy<GenericRepository<Game>> _lazyGameRepository;
        private readonly Lazy<GenericRepository<Genre>> _lazyGenreRepository;
        private readonly Lazy<GenericRepository<Comment>> _lazyCommentRepository;
        private readonly Lazy<GenericRepository<PlatformType>> _lazyPlatformTypeRepository;
        private readonly Lazy<GenericRepository<Order>> _lazyOrderRepository;
        private readonly Lazy<OrderDetailRepository> _lazyOrderDetailRepository;
        private readonly Lazy<GenericRepository<Publisher>> _lazyPublisherRepository;
       // private readonly Lazy<ReadOnlyGenericRepository<Shipper>> _lazyShipperRepository;

        private readonly Lazy<GenericRepository<User>> _lazyUserRepository;
        private readonly Lazy<GenericRepository<Role>> _lazyRoleRepository;

        public UnitOfWork(IDbContext context, IMapper mapper)
        {
            _context = context;
            var mongoDb = new MongoContext();

            _lazyGameRepository = new Lazy<GenericRepository<Game>>(() => new GenericRepository<Game>(_context));
            _lazyGenreRepository = new Lazy<GenericRepository<Genre>>(() => new GenericRepository<Genre>(_context));
            _lazyCommentRepository = new Lazy<GenericRepository<Comment>>(() => new GenericRepository<Comment>(_context));
            _lazyPlatformTypeRepository = new Lazy<GenericRepository<PlatformType>>(() => new GenericRepository<PlatformType>(_context));
            _lazyOrderDetailRepository = new Lazy<OrderDetailRepository>(() => new OrderDetailRepository(_context));
            _lazyOrderRepository = new Lazy<GenericRepository <Order>>(() => new GenericRepository<Order>(_context));
            _lazyPublisherRepository = new Lazy<GenericRepository<Publisher>>(() => new GenericRepository<Publisher>(_context));
          //  _lazyShipperRepository = new Lazy<ReadOnlyGenericRepository<Shipper>>(() => new ReadOnlyGenericRepository<Shipper>(mongoDb));
            _lazyUserRepository = new Lazy<GenericRepository<User>>(() => new GenericRepository<User>(_context));
            _lazyRoleRepository = new Lazy<GenericRepository<Role>>(() => new GenericRepository<Role>(_context));
        }

        public IGenericRepository<Game> Games => _lazyGameRepository.Value;

        public IGenericRepository<Genre> Genres => _lazyGenreRepository.Value;

        public IGenericRepository<Comment> Comments => _lazyCommentRepository.Value;

        public IGenericRepository<PlatformType> PlatformTypes => _lazyPlatformTypeRepository.Value;

        public IGenericRepository<OrderDetail> OrderDetails => _lazyOrderDetailRepository.Value;

        public IGenericRepository<Order> Orders => _lazyOrderRepository.Value;

        public IGenericRepository<Publisher> Publishers => _lazyPublisherRepository.Value;

        //public IGenericRepository<Shipper> Shippers => _lazyShipperRepository.Value;

        public IGenericRepository<User> Users => _lazyUserRepository.Value;

        public IGenericRepository<Role> Roles => _lazyRoleRepository.Value;

        public void Save() => _context.SaveChanges();
    }
}
