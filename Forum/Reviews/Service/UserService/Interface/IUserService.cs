using System.Collections.Generic;
using Reviews.Models;
using Reviews.Mongo;

namespace Reviews.Service.UserService.Interface
{
    public interface IUserService
    {
        string Login(IEnumerable<User> users);
        void Register(List<User> users, Repository<User> repositoryUser);
    }
}