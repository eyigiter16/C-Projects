using System.Collections.Generic;
using Reviews.Models;

namespace Reviews.UserDatabase.Interface
{
    public interface IUserDatabase
    {
        void ReadUser(List<User> users);
        void WriteUser(User user);
    }
}