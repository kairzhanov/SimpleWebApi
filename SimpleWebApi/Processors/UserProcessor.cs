using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using Entities.Models;
using Repository;
using System.Linq;

namespace SimpleWebApi.Processors
{
    public class UserProcessor
    {
        private readonly MySqlConnection _db;
        private readonly ILogger<UserProcessor> _logger;
        private readonly IUserRepository _userRepository;
        
        public UserProcessor(MySqlConnection db, ILogger<UserProcessor> logger, IUserRepository userRepository)
        {
            _db = db;
            _logger = logger;
            _userRepository = userRepository;
        }
        public async Task<List<User>> GetAllUsers()
        { 
            var users = await _userRepository.FindAll();
            return users.ToList();
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await _userRepository.FindById(userId);
            return user;
        }

        private User GetModel(MySqlDataReader reader)
        {
            try
            {
                User entity= new User();
                entity.UserId = (int)reader["UserId"];
                entity.Name= (string)reader["Name"];
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        
     }
}