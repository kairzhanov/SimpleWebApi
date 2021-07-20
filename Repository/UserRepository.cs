using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
    
        private readonly MySqlConnection _db;
        private readonly ILogger<UserRepository> _logger;
        
        public UserRepository(MySqlConnection db, ILogger<UserRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        
        
        public async Task<IEnumerable<User>> FindAll()
        {
            List<User> result = new List<User>();
            
            if (_db.State == ConnectionState.Closed)
                await _db.OpenAsync();

            using var command = new MySqlCommand(@"SELECT * FROM Users;", _db);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                User entity = GetModel(reader);
                result.Add(entity);
            }
            await _db.CloseAsync();
            return result;
        }

        public Task<IEnumerable<User>> FindByConditionAsync(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindById(int id)
        {
            if (_db.State == ConnectionState.Closed)
                await _db.OpenAsync();

            using var command = new MySqlCommand(@"SELECT * FROM Users WHERE UserId=@id;", _db);
            BindId(command, id);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                User entity = GetModel(reader);
                return entity;
            }
            await _db.CloseAsync();

            return null;
        }

        public async Task<int> CreateAsync(User entity)
        {
            if (_db.State == ConnectionState.Closed)
                await _db.OpenAsync();
            
            int result = -1;

            using var cmd = new MySqlCommand(
                @"INSERT INTO `Users` (`Name`, `IsDeleted`, `Phone`) VALUES (@name, @isDeleted, @phone);",
                _db);
            BindParams(cmd, entity);
            await cmd.ExecuteNonQueryAsync();
            result = (int) cmd.LastInsertedId;

            await _db.CloseAsync();
            return result;
        }

        public async Task<int> UpdateAsync(User entity)
        {
            if (_db.State == ConnectionState.Closed)
                await _db.OpenAsync();
            
            int result = -1;

            using var cmd = new MySqlCommand(
                @"UPDATE `Users` SET `Name` = @name, `Phone` = @phone WHERE `UserId` = @id;",
                _db);
            
            BindParams(cmd, entity);
            BindId(cmd, entity.UserId);
            
            await cmd.ExecuteNonQueryAsync();
                       
            await _db.CloseAsync();

            return 0;
        }

        public async Task<int> DeleteAsync(User entity)
        {
            if (_db.State == ConnectionState.Closed)
                await _db.OpenAsync();
            
            int result = -1;

            using var cmd = new MySqlCommand(
                @"UPDATE `Users` SET `IsDeleted` = 1 WHERE `UserId` = @id;",
                _db);
            
            BindId(cmd, entity.UserId);
            
            await cmd.ExecuteNonQueryAsync();
                       
            await _db.CloseAsync();

            return 0;
        }
        
        private void BindId(MySqlCommand cmd, int userId)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = userId,
            });
        }
        
        private void BindParams(MySqlCommand cmd, User user)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@name",
                DbType = DbType.String,
                Value = user.Name,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@isDeleted",
                DbType = DbType.Boolean,
                Value = user.IsDeleted,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@phone",
                DbType = DbType.String,
                Value = user.Phone
            });
        }
        
        private User GetModel(MySqlDataReader reader)
        {
            try
            {
                User entity= new User();
                entity.UserId = (int) reader["UserId"];
                entity.Name = (string) reader["Name"];
                entity.Phone = (string) reader["Phone"];
                entity.IsDeleted = (bool) reader["IsDeleted"];
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