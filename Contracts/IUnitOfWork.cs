namespace Contracts
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IProductRepository ProductRepository { get; }
        IUserProductRepository UserProductRepository { get; }
    }
}