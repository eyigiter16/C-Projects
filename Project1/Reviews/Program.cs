using System;
using Reviews.Interface;
using Reviews.Models;

namespace Reviews
{
    class Program
    {
        private static int max_user = 516;
        private static int max_review = 1024;
        
        static void Main(string[] args)
        {
            var users = new User[max_user];
            var reviews = new Review[max_review];
            var admins = new Admin[2];
            var userNumber = new ReadWrite().ReadUser(users);
            var reviewNumber = new ReadWrite().ReadReviws(reviews);
            new Controller().CreateAdmin(admins);
            new Controller().Observer(admins, users, reviews, userNumber, reviewNumber);
            new ReadWrite().Write(users, reviews);
        }
    }
}