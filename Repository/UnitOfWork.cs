using System;
using Contracts;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IUserProductRepository UserProductRepository { get; }

        public UnitOfWork(IUserRepository userRepository, IProductRepository productRepository,
            IUserProductRepository userProductRepository)
        {
            UserRepository = userRepository;
            ProductRepository = productRepository;
            UserProductRepository = userProductRepository;
        }
    }
}