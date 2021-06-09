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
            if (File.Exists("users.txt"))
            {
                using (var file = new StreamReader("users.txt"))
                {
                    string ln;
                    while ((ln = file.ReadLine()) != null)
                    {
                        var tokens = ln.Split(' ');
                        var user = new User();
                        user.Id = new Guid(tokens[0]);
                        user.FirstName = tokens[1];
                        user.LastName = tokens[2];
                        user.Email = tokens[3];
                        user.Password = tokens[4];
                        users.Add(user);
                    }

                    file.Close();
                }
            }
        }

        public void ReadReviews(List<Review> reviews)
        {
            if (File.Exists("reviews.txt"))
            {
                using (var file = new StreamReader("reviews.txt"))
                {
                    string ln;
                    while ((ln = file.ReadLine()) != null)
                    {
                        var tokens = ln.Split('|');
                        var review = new Review();
                        review.Id = new Guid(tokens[0]);
                        review.Content = tokens[1];
                        review.Title = tokens[2];
                        review.Star = tokens[3];
                        review.Status = tokens[4];
                        review.RejectReason = tokens[5];
                        review.OperatedBy = new Guid(tokens[6]);
                        reviews.Add(review);
                    }

                    file.Close();
                }
            }
        }
        
        public void Write(List<User> users, List<Review> reviews)
        {
            if (File.Exists("users.txt"))    
            {    
                File.Delete("users.txt");    
            }
            File.Create("users.txt");
            foreach (var currentUser in users)
            {
                using(StreamWriter writetext = new StreamWriter("users.txt"))
                {
                    writetext.WriteLine(Convert.ToString(currentUser.Id)+" "+ currentUser.FirstName +" "+
                                        currentUser.LastName +" "+ currentUser.Email+
                                        currentUser.Password);
                }
            }
            if (File.Exists("reviews.txt"))    
            {    
                File.Delete("reviews.txt");    
            }

            File.Create("reviews.txt");
            foreach (var currentReview in reviews)
            {
                using(StreamWriter writetext = new StreamWriter("reviews.txt"))
                {
                    writetext.WriteLine(Convert.ToString(currentReview.Id) +"|"+ currentReview.Content +"|"+
                                        currentReview.Title +"|"+ currentReview.Star +"|"+
                                        currentReview.Status +"|"+ currentReview.RejectReason
                                        +"|"+ currentReview.OperatedBy);
                }
            }
        }
    }
}