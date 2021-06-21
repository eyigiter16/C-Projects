using System.Collections.Generic;
using Reviews.Models;
using Reviews.Mongo;

namespace Reviews.UserOperations.Interface
{
    public interface IUserOperations
    {
        string Login(IEnumerable<User> users);
        void Register(List<User> users, Repository<User> repositoryUser);
    }
}