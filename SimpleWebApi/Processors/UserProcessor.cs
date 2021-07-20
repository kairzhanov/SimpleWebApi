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

        public async Task<User> CreateUser(User user)
        {
            var newUserId = await _userRepository.CreateAsync(user);
            user.UserId = newUserId;
            return user;
        }

        public async Task<int> UpdateUser(User user)
        {
            var result = await _userRepository.UpdateAsync(user);
            return result;
        }

        public async Task<int> DeleteUser(int userId)
        {
            var user = await _userRepository.FindById(userId);
            if (user == null)
                return -1;

            var result = await _userRepository.DeleteAsync(user);
            return result;
        }
     }
}