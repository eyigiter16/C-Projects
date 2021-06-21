using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MongoDB.Driver;
using Reviews.Models;
using Reviews.Mongo;
using Reviews.UserDatabase.Interface;

namespace Reviews.UserDatabase
{
    public class UserDatabase : IUserDatabase
    {
        private readonly Repository<User> _repositoryUser;

        public UserDatabase(Repository<User> repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }
        public void ReadUser(List<User> users)
        {
            var filter = Builders<User>.Filter.Empty;
            var data = _repositoryUser.ReadRecord(filter);
            users.AddRange(data.Select(user => new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password
            }));
        }

        public void WriteUser(User user)
        {
            _repositoryUser.CreateRecord(user);
        }
        //dependency injection for console application
    }
}