using System;
using System.Collections.Generic;
using System.IO;
using Reviews.Models;
using Reviews.ReadWrite.Interface;

namespace Reviews.ReadWrite
{
    public class ReadWrite : IReadWrite
    {
        public void ReadUser(List<User> users)
        {
            if (!File.Exists("users.txt")) return;
            using var file = new StreamReader("users.txt");
            string ln;
            while ((ln = file.ReadLine()) != null)
            {
                var tokens = ln.Split('|');
                var user = new User
                {
                    Id = new Guid(tokens[0]),
                    FirstName = tokens[1],
                    LastName = tokens[2],
                    Email = tokens[3],
                    Password = tokens[4]
                };
                users.Add(user);
            }

            file.Close();
        }

        public void ReadReviews(List<Review> reviews)
        {
            if (!File.Exists("reviews.txt")) return;
            using var file = new StreamReader("reviews.txt");
            string ln;
            while ((ln = file.ReadLine()) != null)
            {
                var tokens = ln.Split('|');
                var review = new Review
                {
                    Id = new Guid(tokens[0]),
                    Content = tokens[1],
                    Title = tokens[2],
                    Star = Convert.ToInt32(tokens[3]),
                    Status = tokens[4],
                    RejectReason = tokens[5],
                    OperatedBy = new Guid(tokens[6])
                };
                reviews.Add(review);
            }
            file.Close();
        }
        
        public void WriteUser(List<User> users)
        {
            File.Delete("users.txt");
            using StreamWriter file1 = new("users.txt");
            foreach (var currentUser in users)
            { 
                file1.WriteLineAsync(Convert.ToString(currentUser.Id) + "|" + currentUser.FirstName 
                                          + "|" + currentUser.LastName + "|" + currentUser.Email + "|" +
                                        currentUser.Password);
            }
        }
        
        public void WriteReview(List<Review> reviews)
        {
            File.Delete("reviews.txt");
            using StreamWriter file1 = new("reviews.txt");
            foreach (var currentReview in reviews)
            {
                file1.WriteLineAsync(Convert.ToString(currentReview.Id) +"|"+ currentReview.Content +"|"+
                                     currentReview.Title +"|"+ currentReview.Star +"|"+
                                     currentReview.Status +"|"+ currentReview.RejectReason
                                     +"|"+ currentReview.OperatedBy);
            }
        }
    }
}