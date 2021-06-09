using System.Collections.Generic;
using Reviews.Models;

namespace Reviews
{
    class Program
    {
        static void Main(string[] args)
        {
            var users = new List<User>();
            var reviews = new List<Review>();
            var admins = new List<Admin>();
            new ReadWrite.ReadWrite().ReadUser(users);
            new ReadWrite.ReadWrite().ReadReviews(reviews);
            new Controller.Controller().CreateAdmin(admins);
            new Controller.Controller().Observer(admins, users, reviews);
            new ReadWrite.ReadWrite().Write(users, reviews);
        }
    }
}