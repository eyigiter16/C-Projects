using System;
using System.Collections.Generic;
using Reviews.Controller.Interface;
using Reviews.Models;

namespace Reviews.Controller
{
    public class Controller : IController
    {
        public void Observer(List<Admin> admins, List<User>  users, List<Review>  reviews)
        {
            var looper = true;
            while (looper)
            {
                Console.WriteLine("For User Login type: 1\n" +
                                  "For Registration type: 2\n" +
                                  "To show reviews type: 3\n" +
                                  "For Admin Login type: 4\n" +
                                  "To exit type: 5");
                var choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        var id = Login(users);
                        while (id != null)
                        {
                            Console.WriteLine("To see the reviews type: reviews" +
                                              "\nTo change a review type: change" +
                                              "\nTo create new review type: new" +
                                              "\nTo logout and return bak to main menu type: exit");
                            var userChoice = Console.ReadLine().ToLower();
                            if (string.Equals(userChoice, "reviews"))
                            {
                                ReadUserReview(reviews, id);
                            }
                            else if (string.Equals(userChoice, "change"))
                            {
                                ChangeReview(reviews, id);
                            }
                            else if (string.Equals(userChoice, "new"))
                            {
                                CreateReview(reviews, id);
                            }
                            else if (string.Equals(userChoice, "exit"))
                            {
                                id = null;
                            }
                            
                        }
                        break;
                    case 2:
                        Register(users);
                        break;
                    case 3:
                        ReadAllReview(reviews);
                        break;
                    case 4:
                        while (LoginAdmin(admins) == 1)
                        {
                            Console.WriteLine("To see the reviews type: reviews" +
                                              "\nTo change status of reviews type: change" +
                                              "\nTo logout and return back to main menu type: exit");
                            var adminChoice = Console.ReadLine().ToLower();
                            if (string.Equals(adminChoice, "reviews"))
                            {
                                ReadAdminByStatus(reviews);
                            }
                            else if (string.Equals(adminChoice, "change"))
                            {
                                ChangeStatus(reviews);
                            }
                            else if (string.Equals(adminChoice, "exit"))
                            {
                                break;
                            }
                        }
                        break;
                    case 5:
                        looper = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }

        }

        public string Login(List<User> users)
        {
            Console.WriteLine("Please enter your E-mail:");
            var e = Console.ReadLine();
            Console.WriteLine("Please enter your Password:");
            var p = Console.ReadLine();
            foreach (var user in users)
            {
                if (string.Equals(user.Email, e) )
                {
                    if (String.Equals(user.Password, p))
                    {
                        return Convert.ToString(user.Id);
                    }
                    else
                    {
                        Console.WriteLine("Wrong Password!");
                    }
                }
            }
            Console.WriteLine("Invalid user login attempt!\nReturning to main menu.");
            return null;
        }

        public void Register(List<User> users)
        {
            var id = Guid.NewGuid();
            var user = new User();
            user.Id = id;
            Console.WriteLine("Please enter your first name:");
            user.FirstName = Console.ReadLine();
            Console.WriteLine("Please enter your last name:");
            user.LastName = Console.ReadLine();
            Console.WriteLine("Please enter your email:");
            user.Email = Console.ReadLine();
            Console.WriteLine("Please enter your password:");
            user.Password = Console.ReadLine();
            users.Add(user);

        }

        public int LoginAdmin(List<Admin> admins)
        {
            Console.WriteLine("Please enter your User Name:");
            var u = Console.ReadLine();
            Console.WriteLine("Please enter your Password:");
            var p = Console.ReadLine();
            foreach (var admin in admins)
            {
                if (string.Equals(admin.UserName, u) )
                {
                    if (String.Equals(admin.Password, p))
                    {
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("Wrong Password!");
                    }
                }
            }
            Console.WriteLine("Invalid admin login attempt!\n Returning to main menu.");
            return 0;
        }

        public void CreateAdmin(List<Admin> admins)
        {
            for (var i = 0; i < 2; i++)
            {
                var id = Guid.NewGuid();
                var admin = new Admin();
                admin.Id = id;
                admin.UserName = "admin"+Convert.ToString(i+1);
                admin.Password = "1234";
                admins.Add(admin);
            }
        }

        public void ReadUserReview(List<Review>  reviews, string id)
        {
            var looper = true;
            while (looper)
            {
                Console.WriteLine("To show reviews type: reviews" +
                                  "\nTo return back type: back");
                var choice = Console.ReadLine().ToLower();
                
                if (string.Equals(choice, "back"))
                {
                    looper = false;
                }
                else if(string.Equals(choice, "reviews"))
                {
                    foreach (var review in reviews)
                    {
                        if (string.Equals(Convert.ToString(review.OperatedBy), id))
                        {
                            Console.WriteLine("ID: "+ Convert.ToString(review.Id)+"Title: " + review.Title + "|| Content: " + review.Content + "|| Star: " +
                                              review.Star + "|| Status: " + review.Status + "|| Operated by: " + review.OperatedBy);
                        }
                    }
                }
            }
        }

        public void ReadAllReview(List<Review>  reviews)
        {
            var looper = true;
            while (looper)
            {
                Console.WriteLine("To show reviews type: reviews" +
                                  "\nTo return back type: back");
                var choice = Console.ReadLine().ToLower();
                
                if (string.Equals(choice, "back"))
                {
                    looper = false;
                }
                else if(string.Equals(choice, "reviews"))
                {
                    foreach (var review in reviews)
                    {
                        if (String.Equals(review.Status, "approved"))
                        {
                            Console.WriteLine("ID: "+ Convert.ToString(review.Id)+"Title: " + review.Title + "|| Content: " + review.Content + "|| Star: " +
                                              review.Star + "|| Status: " + review.Status + "|| Operated by: " + review.OperatedBy);
                        }
                    }
                }
            }
        }

        public void ReadAdminByStatus(List<Review>  reviews)
        {
            var looper = true;
            while (looper)
            {
                Console.WriteLine("To show approved or pending or rejected or all to the whole word: " +
                                  "\nTo return back type back: ");
                var choice = Console.ReadLine().ToLower();
                
                if (string.Equals(choice, "all"))
                {
                    foreach (var review in reviews)
                    {
                        Console.WriteLine("ID: "+ Convert.ToString(review.Id)+"Title: " + review.Title + "|| Content: " + review.Content + "|| Star: " +
                                          review.Star + "|| Status: " + review.Status + "|| Operated by: " + review.OperatedBy);
                    }
                }
                else if (string.Equals(choice, "back"))
                {
                    looper = false;
                }
                else
                {
                    foreach (var review in reviews)
                    {
                        if (String.Equals(review.Status, choice))
                        {
                            Console.WriteLine("ID: "+ Convert.ToString(review.Id)+"Title: " + review.Title + "|| Content: " + review.Content + "|| Star: " +
                                              review.Star + "|| Status: " + review.Status + "|| Operated by: " + review.OperatedBy);
                        }
                    }
                }
            }
        }

        public void ChangeStatus(List<Review>  reviews)
        {
            Console.WriteLine("Please enter the title of the review:");
            var idRejected = Console.ReadLine();
            foreach (var review in reviews)
            {
                if (string.Equals(idRejected, review.Title))
                {
                    Console.WriteLine("ID: "+ idRejected +"\nTitle: " + review.Title + "\nContent: " + review.Content + "\nStar: " +
                                      review.Star + "\nStatus: " + review.Status + "\nOperated by: " + review.OperatedBy);
                    if (string.Equals(review.Status.ToLower(), "rejected"))
                    {
                        Console.WriteLine("Reject Reason: " + review.RejectReason);
                    }
                    Console.WriteLine("Enter new status:");
                    var newStat = Console.ReadLine();
                    Console.WriteLine("Enter reject reason (empty if not rejected):");
                    var rejectReason = Console.ReadLine();
                    review.Status = newStat;
                    review.RejectReason = rejectReason;
                }
            }
        }

        public void ChangeReview(List<Review>  reviews, string id)
        {
            Console.WriteLine("Please enter the Title of the review:");
            var idChange = Console.ReadLine();
            foreach (var review in reviews)
            {
                if (string.Equals(idChange, review.Title))
                {
                    if (string.Equals(Convert.ToString(review.OperatedBy), id))
                    {
                        Console.WriteLine("ID: "+ idChange +"\nTitle: " + review.Title + "\nContent: " + review.Content + "\nStar: " +
                                          review.Star + "\nStatus: " + review.Status + "\nOperated by: " + review.OperatedBy);
                        if (string.Equals(review.Status.ToLower(), "rejected"))
                        {
                            Console.WriteLine("Reject Reason: " + review.RejectReason);
                        }
                        Console.WriteLine("Enter new Title:");
                        var newTitle = Console.ReadLine();
                        Console.WriteLine("Enter new Content:");
                        var newContent = Console.ReadLine();
                        Console.WriteLine("Enter new Star:");
                        var newStar = Console.ReadLine();
                        review.Title = newTitle;
                        review.Content = newContent;
                        review.Star = newStar;
                        review.Status = "pending";
                        review.RejectReason = null;
                    }
                    else
                    {
                        Console.WriteLine("This comment does not belong to you");
                    }
                    
                }
            }
        }

        public void CreateReview(List<Review>  reviews, string id)
        {
            Console.WriteLine("Enter new Title:"); 
            var newTitle = Console.ReadLine(); 
            Console.WriteLine("Enter new Content:"); 
            var newContent = Console.ReadLine(); 
            Console.WriteLine("Enter new Star:"); 
            var newStar = Console.ReadLine();
            var review = new Review();
            review.Title = newTitle; 
            review.Content = newContent; 
            review.Star = newStar; 
            review.Status = "pending"; 
            review.OperatedBy = new Guid(id); 
            review.RejectReason = null;
            reviews.Add(review);
        }
    }
}