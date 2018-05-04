using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Repositories;
using System;
using AutoMapper;
using GameStore.DAL.Mongo;
using GameStore.DAL.Mongo.MongoEntities;
using log4net;

namespace GameStore.DAL.EF
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly IDbContext _context;

        private readonly Lazy<GenericRepository<Game>> _lazyGameRepository;
        private readonly Lazy<GenericRepository<Genre>> _lazyGenreRepository;
        private readonly Lazy<GenericRepository<Comment>> _lazyCommentRepository;
        private readonly Lazy<GenericRepository<PlatformType>> _lazyPlatformTypeRepository;
        private readonly Lazy<GenericRepository<OrderDetail>> _lazyOrderDetailRepository;
        private readonly Lazy<OrderDecoratorRepository> _lazyOrderRepository;
        private readonly Lazy<GenericRepository<Publisher>> _lazyPublisherRepository;
        private readonly Lazy<ReadOnlyGenericRepository<Shipper>> _lazyShipperRepository;

        public UnitOfWork(IDbContext context, IMapper mapper, ILog log)
        {
            _context = context;
            var mongoDb = new MongoContext();

            _lazyGameRepository = new Lazy<GenericRepository<Game>>(() => new GenericRepository<Game>(_context, mongoDb, log));
            _lazyGenreRepository = new Lazy<GenericRepository<Genre>>(() => new GenericRepository<Genre>(_context, mongoDb, log));
            _lazyCommentRepository = new Lazy<GenericRepository<Comment>>(() => new GenericRepository<Comment>(_context, mongoDb, log));
            _lazyPlatformTypeRepository = new Lazy<GenericRepository<PlatformType>>(() => new GenericRepository<PlatformType>(_context, mongoDb, log));
            _lazyOrderDetailRepository = new Lazy<GenericRepository<OrderDetail>>(() => new GenericRepository<OrderDetail>(_context, mongoDb, log));
            _lazyOrderRepository = new Lazy<OrderDecoratorRepository>(() => new OrderDecoratorRepository(_context, mongoDb, mapper));
            _lazyPublisherRepository = new Lazy<GenericRepository<Publisher>>(() => new GenericRepository<Publisher>(_context, mongoDb, log));
            _lazyShipperRepository = new Lazy<ReadOnlyGenericRepository<Shipper>>(() => new ReadOnlyGenericRepository<Shipper>(mongoDb));
        }

        public IGenericRepository<Game> Games => _lazyGameRepository.Value;

        public IGenericRepository<Genre> Genres => _lazyGenreRepository.Value;

        public IGenericRepository<Comment> Comments => _lazyCommentRepository.Value;

        public IGenericRepository<PlatformType> PlatformTypes => _lazyPlatformTypeRepository.Value;

        public IGenericRepository<OrderDetail> OrderDetails => _lazyOrderDetailRepository.Value;

        public IGenericRepository<Order> Orders => _lazyOrderRepository.Value;

        public IGenericRepository<Publisher> Publishers => _lazyPublisherRepository.Value;

        public IGenericRepository<Shipper> Shippers => _lazyShipperRepository.Value;

        public void Save() => _context.SaveChanges();
    }
}
