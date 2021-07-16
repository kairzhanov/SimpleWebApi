using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IUserProductRepository : IRepositoryBase<UserProductDto>
    {
        public Task<List<UserProductDto>> GetUserProductsAsync(int userId);
    }
}