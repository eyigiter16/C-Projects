﻿using System.Collections.Generic;
using Reviews.Models;

namespace Reviews
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var users = new List<User>();
            var reviews = new List<Review>();
            var admins = new List<Admin>();
            new ReadWrite.ReadWrite().ReadUser(users);
            new ReadWrite.ReadWrite().ReadReviews(reviews);
            new Controller.Controller().CreateAdmin(admins);
            new Controller.Controller().Observer(admins, users, reviews);
        }
    }
}