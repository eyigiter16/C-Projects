using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Reviews.Models;
using Reviews.Mongo;
using Reviews.Mongo.Context;
using Reviews.Repository;
using Reviews.Service.AdminService;

namespace Reviews
{
    internal static class Program
    {
        private static void Main()
        {
            var mongo = Configuration.Load();
            var client = new MongoClient(mongo.GetConnectionString("path"));
            var context1 = new Context<User>(client, "Forum");
            var context2 = new Context<Review>(client, "Forum");
            var repositoryUser = new Repository<User>(context1, "Users");
            var repositoryReview = new Repository<Review>(context2, "Reviews");
            var users = new List<User>();
            var reviews = new List<Review>();
            var admins = new List<Admin>();
            new UserRepository(repositoryUser).ReadUser(users);
            new ReviewRepository(repositoryReview).ReadReviews(reviews);
            new AdminService().CreateAdmin(admins);
            new Controller.Controller().Observer(admins, users, reviews, repositoryUser, repositoryReview);
        }
    }
}