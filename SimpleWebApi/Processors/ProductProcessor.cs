using System.Collections.Generic;
using System.Data;
using Entities.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace SimpleWebApi.Processors
{
    public class ProductProcessor
    {
        private readonly MySqlConnection _db;
        private readonly IProductRepository _productRepository;
        
        public ProductProcessor(MySqlConnection db, IProductRepository productRepository)
        {
            _db = db;
            _productRepository = productRepository;
        }

        
        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _productRepository.FindAll();
            return products.ToList();
        }
    }
}