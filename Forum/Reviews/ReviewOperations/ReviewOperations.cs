using System;
using System.Collections.Generic;
using System.Linq;
using Reviews.Models;
using Reviews.Mongo;
using Reviews.ReviewOperations.Interface;

namespace Reviews.ReviewOperations
{
    public class ReviewOperations : IReviewOperations
    {
        public void ReadUserReview(List<Review>  reviews, string id)
        {
            var looper = true;
            while (looper)
            {
                Console.WriteLine("\nTo show only your reviews                    : 1 " +
                                  "\nTo show approved and your review             : 2 " +
                                  "\nTo return back                               : 3 ");
                var choice = Console.ReadLine()?.ToLower();
                Console.Clear();
                var exist = false;
                if (string.Equals(choice, "3"))
                {
                    looper = false;
                    continue;
                }
                else if (string.Equals(choice, "1"))
                {
                    foreach (var review in reviews.Where(review =>
                        string.Equals(Convert.ToString(review.OperatedBy), id)))
                    {
                        Console.WriteLine("\nID: " + Convert.ToString(review.Id) + " || Title: " + review.Title +
                                          " || Content: " + review.Content + " || Star: " +
                                          review.Star + " || Status: " + review.Status + " || Operated by: " +
                                          review.OperatedBy);
                        exist = true;
                    }

                    if (!exist)
                    {
                        Console.WriteLine("\nNo reviews exist yet. ");
                    }
                }
                else if (string.Equals(choice, "2"))
                {
                    foreach (var review in reviews.Where(review =>
                        !string.Equals(Convert.ToString(review.OperatedBy), id) && string.Equals(review.Status, "approved")))
                    {
                        Console.WriteLine("\nID: " + Convert.ToString(review.Id) + " || Title: " + review.Title +
                                          " || Content: " + review.Content + " || Star: " +
                                          review.Star + " || Status: " + review.Status + " || Operated by: " +
                                          review.OperatedBy);
                        exist = true;
                    }
                    foreach (var review in reviews.Where(review =>
                        string.Equals(Convert.ToString(review.OperatedBy), id)))
                    {
                        Console.WriteLine("\nID: " + Convert.ToString(review.Id) + " || Title: " + review.Title +
                                          " || Content: " + review.Content + " || Star: " +
                                          review.Star + " || Status: " + review.Status + " || Operated by: " +
                                          review.OperatedBy);
                        exist = true;
                    }

                    if (!exist)
                    {
                        Console.WriteLine("\nNo reviews exist yet. ");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! \n");
                }
            }
        }

        public void ReadAllReview(List<Review>  reviews)
        {
            var looper = true;
            string choice = null;
            while (looper)
            {
                if (string.Equals(choice, "1"))
                {
                    looper = false;
                    continue;
                }
                var exist = false;
                foreach (var review in reviews.Where(review => string.Equals(review.Status, "approved")))
                {
                    Console.WriteLine("\nID: "+ Convert.ToString(review.Id)+" || Title: " + review.Title + " || Content: " + review.Content + " || Star: " +
                                      review.Star + " || Status: " + review.Status + " || Operated by: " + review.OperatedBy);
                    exist = true;
                }

                if (!exist)
                {
                    Console.WriteLine("\nNo reviews exist yet. ");
                }
                Console.WriteLine("\nTo return back                               : 1 ");
                choice = Console.ReadLine()?.ToLower();
                Console.Clear();
            }
        }

        public void ReadAdminByStatus(List<Review>  reviews)
        {
            var looper = true;
            while (looper)
            {
                Console.WriteLine("\nTo show approved                             : 1 \n" +
                                  "To show pending                              : 2 \n" +
                                  "To show rejected                             : 3 \n" +
                                  "To show all                                  : 4 \n" +
                                  "To return back                               : 5 ");
                var choice = Console.ReadLine()?.ToLower();
                Console.Clear();
                
                if (string.Equals(choice, "4"))
                {
                    if (!reviews.Any())
                    {
                        Console.WriteLine("\nNo reviews exist yet. ");
                        continue;
                    }
                    foreach (var review in reviews)
                    {
                        Console.WriteLine("\nID: "+ Convert.ToString(review.Id)+" || Title: " + review.Title + " || Content: " + review.Content + " || Star: " +
                                          review.Star + " || Status: " + review.Status + " || Operated by: " + review.OperatedBy);
                        if (string.Equals(review.Status.ToLower(), "rejected"))
                        {
                            Console.WriteLine("Reject Reason: " + review.RejectReason);
                        }
                    }
                }
                else if (string.Equals(choice, "5"))
                {
                    looper = false;
                }
                else if (string.Equals(choice, "1"))
                {
                    if (!reviews.Any())
                    {
                        Console.WriteLine("\nNo reviews exist yet. ");
                        continue;
                    }
                    foreach (var review in reviews.Where(review => string.Equals(review.Status, "approved")))
                    {
                        Console.WriteLine("\nID: "+ Convert.ToString(review.Id)+" || Title: " + review.Title + " | Content: " + review.Content + " || Star: " +
                                          review.Star + " || Status: " + review.Status + " || Operated by: " + review.OperatedBy);
                    }
                }
                else if (string.Equals(choice, "2"))
                {
                    if (!reviews.Any())
                    {
                        Console.WriteLine("\nNo reviews exist yet. ");
                        continue;
                    }
                    foreach (var review in reviews.Where(review => string.Equals(review.Status, "pending")))
                    {
                        Console.WriteLine("\nID: "+ Convert.ToString(review.Id)+" || Title: " + review.Title + " | Content: " + review.Content + " || Star: " +
                                          review.Star + " || Status: " + review.Status + " || Operated by: " + review.OperatedBy);
                    }
                }
                else if (string.Equals(choice, "3"))
                {
                    if (!reviews.Any())
                    {
                        Console.WriteLine("\nNo reviews exist yet. ");
                        continue;
                    }
                    foreach (var review in reviews.Where(review => string.Equals(review.Status, "rejected")))
                    {
                        Console.WriteLine("\nID: "+ Convert.ToString(review.Id)+" || Title: " + review.Title + " | Content: " + review.Content + " || Star: " +
                                          review.Star + " || Status: " + review.Status + " || Operated by: " + review.OperatedBy);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! \n");
                }
            }
        }
        
        public void CreateReview(List<Review>  reviews, string id, Repository<Review> repositoryReview)
        {
            
            var reviewId = Guid.NewGuid();
            Console.WriteLine("\nEnter new Title:"); 
            var newTitle = Console.ReadLine(); 
            Console.WriteLine("\nEnter new Content:"); 
            var newContent = Console.ReadLine(); 
            Console.WriteLine("\nEnter new Star (1-5):"); 
            var newStar = Console.ReadLine();
            int star;
            while (true)
            {
                var numbers = Enumerable.Range(1, 5).ToList();
                if (!numbers.Any(x => newStar.EndsWith(x.ToString())) || newStar.Length != 1)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input for star value. Please enter an integer 1-5. \n");
                    Console.WriteLine("\nEnter new Star (1-5):"); 
                    newStar = Console.ReadLine();
                }
                else
                {
                    star = Convert.ToInt32(newStar);
                    break;
                }
            }
            var review = new Review
            {
                Id = reviewId,
                Title = newTitle,
                Content = newContent,
                Star = star,
                Status = "pending",
                OperatedBy = new Guid(id),
                RejectReason = null
            };
            reviews.Add(review);
            Console.Clear();
            new ReviewDatabase.ReviewDatabase(repositoryReview).WriteReview(review);
        }

        public void ChangeStatus(IEnumerable<Review> reviews, Repository<Review> repositoryReview)
        {
            Console.WriteLine("\nPlease enter the title of the review:");
            var idRejected = Console.ReadLine();
            Console.Clear();
            
            foreach (var review in reviews.Where(review => string.Equals(idRejected, review.Title)))
            {
                Console.WriteLine("\nID: "+ review.Id +"\nTitle: " + review.Title + "\nContent: " + review.Content + "\nStar: " +
                                  review.Star + "\nStatus: " + review.Status + "\nOperated by: " + review.OperatedBy);
                if (string.Equals(review.Status.ToLower(), "rejected"))
                {
                    Console.WriteLine("Reject Reason: " + review.RejectReason);
                }
                Console.WriteLine("\nEnter new status. \n" +
                                  "Approved                                     : 1 \n" +
                                  "Rejected                                     : 2 \n");
                var newStat = Console.ReadLine();
                while (true)
                {
                    if (string.Equals(newStat, "1"))
                    {
                        newStat = "approved";
                        break;
                    }
                    if (string.Equals(newStat, "2"))
                    {
                        newStat = "rejected";
                        break;
                    }
                    Console.Clear();
                    Console.WriteLine("Invalid input for stat input. \n");
                    Console.WriteLine("\nEnter new status. \n" +
                                      "Approved                                     : 1 \n" +
                                      "Rejected                                     : 2 \n"); 
                    newStat = Console.ReadLine();
                }
                if (string.Equals(newStat, "rejected"))
                {
                    Console.WriteLine("\nEnter reject reason (empty if not rejected):");
                    var rejectReason = Console.ReadLine();
                    review.RejectReason = rejectReason;
                }
                review.Status = newStat;
                Console.Clear();
                new ReviewDatabase.ReviewDatabase(repositoryReview).UpdateReview(review);
            }
        }

        public void ChangeReview(List<Review>  reviews, string id, Repository<Review> repositoryReview)
        {
            while (true)
            {
                Console.WriteLine("\nPlease enter the Title of the review:");
                var idChange = Console.ReadLine();
                Console.Clear();
                var inLoop = false;

                foreach (var review in reviews.Where(review => string.Equals(idChange, review.Title)))
                {
                    inLoop = true;
                    if (string.Equals(Convert.ToString(review.OperatedBy), id))
                    {
                        Console.WriteLine("\nID: " + review.Id + "\nTitle: " + review.Title + "\nContent: " +
                                          review.Content + "\nStar: " +
                                          review.Star + "\nStatus: " + review.Status + "\nOperated by: " +
                                          review.OperatedBy);
                        if (string.Equals(review.Status.ToLower(), "rejected"))
                        {
                            Console.WriteLine("Reject Reason: " + review.RejectReason);
                        }

                        var exitLoop = false;
                        var changesDone = false;
                        while (!exitLoop)
                        {
                            Console.WriteLine("\nTo change the Title                          : 1 \n" +
                                              "To change the Content                        : 2 \n" +
                                              "To change the Star                           : 3 \n" +
                                              "To save the changes and exit                 : 4 ");
                            var choiceReview = Console.ReadLine();
                            Console.Clear();

                            if (string.Equals(choiceReview, "1"))
                            {
                                Console.WriteLine("\nEnter new Title:");
                                var newTitle = Console.ReadLine();
                                review.Title = newTitle;
                                changesDone = true;
                                Console.Clear();
                            }
                            else if (string.Equals(choiceReview, "2"))
                            {
                                Console.WriteLine("\nEnter new Content:");
                                var newContent = Console.ReadLine();
                                review.Content = newContent;
                                changesDone = true;
                                Console.Clear();
                            }
                            else if (string.Equals(choiceReview, "3"))
                            {
                                Console.WriteLine("\nEnter new Star (1-5):");
                                var newStar = Console.ReadLine();
                                int star;
                                while (true)
                                {
                                    var numbers = Enumerable.Range(1, 5).ToList();
                                    if (!numbers.Any(x => newStar.EndsWith(x.ToString())) || newStar.Length != 1)
                                    {
                                        Console.Clear();
                                        Console.WriteLine(
                                            "Invalid input for star value. Please enter an integer 1-5. \n");
                                        Console.WriteLine("\nEnter new Star (1-5):");
                                        newStar = Console.ReadLine();
                                    }
                                    else
                                    {
                                        star = Convert.ToInt32(newStar);
                                        break;
                                    }
                                }

                                review.Star = star;
                                changesDone = true;
                                Console.Clear();
                            }
                            else if (string.Equals(choiceReview, "4"))
                            {
                                exitLoop = true;
                            }
                            else
                            {
                                Console.WriteLine("\nInvalid input! \n" +
                                                  "To logout and return back to main menu       : 1 \n" +
                                                  "To make a new choice                         : (just enter) ");
                                var again = Console.ReadLine()?.ToLower();
                                Console.Clear();

                                if (string.Equals(again, "1"))
                                {

                                    Console.WriteLine("\nExiting. ");
                                    exitLoop = true;
                                }
                            }

                            if (!changesDone) continue;
                            review.Status = "pending";
                            review.RejectReason = null;
                            new ReviewDatabase.ReviewDatabase(repositoryReview).UpdateReview(review);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nThis comment does not belong to you");
                    }
                }

                if (inLoop == false)
                {
                    Console.WriteLine("\nInvalid input! \n" +
                                      "To logout and return back to main menu       : 1 \n" +
                                      "To give a new input                          : (just enter) ");
                    var again = Console.ReadLine()?.ToLower();
                    Console.Clear();

                    if (!string.Equals(again, "1")) continue;
                    Console.WriteLine("\nExiting. ");
                }
                break;
            }
        }
    }
}