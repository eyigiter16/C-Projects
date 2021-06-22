using System.Collections.Generic;
using Reviews.Models;

namespace Reviews.Repository.Interface
{
    public interface IUserRepository
    {
        void ReadUser(List<User> users);
        void WriteUser(User user);
    }
}