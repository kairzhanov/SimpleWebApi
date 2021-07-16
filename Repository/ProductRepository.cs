using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySqlConnection _db;
        private readonly ILogger<ProductRepository> _logger;
        
        public ProductRepository(MySqlConnection db, ILogger<ProductRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<IEnumerable<Product>> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> FindByConditionAsync(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<Product> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}