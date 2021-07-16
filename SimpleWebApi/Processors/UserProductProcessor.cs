using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using MySqlConnector;
using Entities.Models;

namespace SimpleWebApi.Processors
{
    public class UserProductProcessor
    {
        private readonly MySqlConnection _db;
        private readonly IUserProductRepository _userProductRepository;
        
        public UserProductProcessor(MySqlConnection db, IUserProductRepository userProductRepository)
        {
            _db = db;
            _userProductRepository = userProductRepository;
        }

        public async Task<List<UserProductDto>> GetAllUserProductsAsync()
        {
            var userProducts = await _userProductRepository.FindAll();

            return userProducts.ToList();
        }
        
        public async Task<List<UserProductDto>> GetUserProductsAsync(int userId)
        {
            var userProducts = await _userProductRepository.GetUserProductsAsync(userId);
            return userProducts.ToList();
        }

        
    }
}