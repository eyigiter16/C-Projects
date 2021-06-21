﻿using System.Collections.Generic;
using MongoDB.Driver;
using Reviews.Models;
using Reviews.Mongo;
using Reviews.Mongo.Context;

namespace Reviews
{
    internal static class Program
    {
        private static void Main()
        {
            var client = new MongoClient("mongodb+srv://ekrem:eko1234@cluster0.byr3l.mongodb.net/Forum?retryWrites=true&w=majority");
            var context1 = new Context<User>(client, "Forum");
            var context2 = new Context<Review>(client, "Forum");
            var repositoryUser = new Repository<User>(context1, "Users");
            var repositoryReview = new Repository<Review>(context2, "Reviews");
            var users = new List<User>();
            var reviews = new List<Review>();
            var admins = new List<Admin>();
            new UserDatabase.UserDatabase(repositoryUser).ReadUser(users);
            new ReviewDatabase.ReviewDatabase(repositoryReview).ReadReviews(reviews);
            new Controller.Controller().CreateAdmin(admins);
            new Controller.Controller().Observer(admins, users, reviews, repositoryUser, repositoryReview);
        }
    }
}