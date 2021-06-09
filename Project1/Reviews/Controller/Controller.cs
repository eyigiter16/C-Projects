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
                Console.WriteLine("\nFor User Login                               : 1\n" +
                                  "For Registration                             : 2\n" +
                                  "To show reviews                              : 3\n" +
                                  "For Admin Login                              : 4\n" +
                                  "To exit                                      : 5");
                var choice = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                switch (choice)
                {
                    case 1:
                        var id = Login(users);
                        var mistakeCounter = 0;
                        while (id != null)
                        {
                            Console.WriteLine("\nTo see the reviews                           : reviews\n" +
                                              "To change a review                           : change\n" +
                                              "To create new review                         : new\n" +
                                              "To logout and return back to main menu       : exit");
                            var userChoice = Console.ReadLine().ToLower();
                            Console.Clear();
                            
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
                            else
                            {
                                mistakeCounter++;
                                if (mistakeCounter==2)
                                {
                                    
                                    Console.WriteLine("\nInvalid input again! \n" +
                                                      "Returning back to main menu. ");
                                    id = null;
                                }
                                else
                                {
                                    Console.WriteLine("\nInvalid input! \n" +
                                                      "If you give another wrong input you \n" +
                                                      "will be sent back to main menu. \n" +
                                                      "To logout and return back to main menu       : exit \n" +
                                                      "To make a new choice                         : continue ");
                                    var mistakeChoice = Console.ReadLine().ToLower();
                                    Console.Clear();
                                    
                                    if (string.Equals(mistakeChoice, "exit"))
                                    {
                                        
                                        Console.WriteLine("\nReturning back to main menu. ");
                                        id = null;
                                    }
                                    else if (!string.Equals(mistakeChoice, "continue"))
                                    {
                                        
                                        Console.WriteLine("\nInvalid input again! \n" +
                                                          "Returning back to main menu. ");
                                        id = null;
                                    }
                                }
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
                        var checkAdmin = LoginAdmin(admins);
                        var mistakeCounter1 = 0;
                        while (checkAdmin == 1)
                        {
                            Console.WriteLine("\nTo see the reviews                           : reviews\n" +
                                              "To change status of reviews                  : change\n" +
                                              "To logout and return back to main menu       : exit");
                            var adminChoice = Console.ReadLine().ToLower();
                            Console.Clear();
                            
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
                                checkAdmin = 0;
                            }
                            else
                            {
                                mistakeCounter1++;
                                if (mistakeCounter1==2)
                                {
                                    
                                    Console.WriteLine("\nInvalid input again! \n" +
                                                      "Returning back to main menu. ");
                                    checkAdmin = 0;
                                }
                                else
                                {
                                    Console.WriteLine("\nInvalid input! \n" +
                                                      "If you give another wrong input you \n" +
                                                      "will be sent back to main menu. \n" +
                                                      "To logout and return back to main menu       : exit \n" +
                                                      "To make a new choice                         : continue ");
                                    var mistakeChoice = Console.ReadLine().ToLower();
                                    Console.Clear();
                                    
                                    if (string.Equals(mistakeChoice, "exit"))
                                    {
                                        
                                        Console.WriteLine("\nReturning back to main menu. ");
                                        checkAdmin = 0;
                                    }
                                    else if (!string.Equals(mistakeChoice, "continue"))
                                    {
                                        
                                        Console.WriteLine("\nInvalid input again! \n" +
                                                          "Returning back to main menu. ");
                                        checkAdmin = 0;
                                    }
                                }
                            }
                        }
                        break;
                    case 5:
                        
                        Console.WriteLine("\nExiting. ");
                        looper = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input! \n" +
                                          "To logout and return back to main menu       : exit \n" +
                                          "To make a new choice                         : (whatever else) ");
                        var again = Console.ReadLine().ToLower();
                        Console.Clear();
                        
                        if (string.Equals(again, "exit"))
                        {
                            
                            Console.WriteLine("\nExiting. "); 
                            looper = false;
                        }
                        break;
                }
            }

        }

        public string Login(List<User> users)
        {
            Console.WriteLine("\nPlease enter your E-mail: ");
            var e = Console.ReadLine();
            Console.WriteLine("\nPlease enter your Password: ");
            var p = Console.ReadLine();
            Console.Clear();
            
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
                        
                        Console.WriteLine("\nWrong Password! ");
                    }
                }
            }
            
            Console.WriteLine("\nInvalid user login attempt! \nReturning to main menu. ");
            return null;
        }

        public void Register(List<User> users)
        {
            var id = Guid.NewGuid();
            var user = new User();
            user.Id = id;
            Console.WriteLine("\nPlease enter your first name: ");
            user.FirstName = Console.ReadLine();
            Console.WriteLine("\nPlease enter your last name: ");
            user.LastName = Console.ReadLine();
            Console.WriteLine("\nPlease enter your email: ");
            user.Email = Console.ReadLine();
            Console.WriteLine("\nPlease enter your password: ");
            user.Password = Console.ReadLine();
            users.Add(user);
            Console.Clear();
        }

        public int LoginAdmin(List<Admin> admins)
        {
            Console.WriteLine("\nPlease enter your User Name:");
            var u = Console.ReadLine();
            Console.WriteLine("\nPlease enter your Password:");
            var p = Console.ReadLine();
            Console.Clear();
            
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
                        Console.WriteLine("\nWrong Password! ");
                    }
                }
            }
            
            Console.WriteLine("\nInvalid admin login attempt!\n Returning to main menu.");
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
                Console.WriteLine("\nTo show reviews                              : reviews \n" +
                                  "\nTo return back                               : back ");
                var choice = Console.ReadLine().ToLower();
                Console.Clear();

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
                            Console.WriteLine("\nID: "+ Convert.ToString(review.Id)+" || Title: " + review.Title + " || Content: " + review.Content + " || Star: " +
                                              review.Star + " || Status: " + review.Status + " || Operated by: " + review.OperatedBy);
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
                Console.WriteLine("\nTo show reviews                              : reviews \n" +
                                  "\nTo return back                             : back");
                var choice = Console.ReadLine().ToLower();
                Console.Clear();
                
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
                            Console.WriteLine("\nID: "+ Convert.ToString(review.Id)+" || Title: " + review.Title + " || Content: " + review.Content + " || Star: " +
                                              review.Star + " || Status: " + review.Status + " || Operated by: " + review.OperatedBy);
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
                Console.WriteLine("\nTo show approved                             : approved \n" +
                                  "To show pending                              : pending \n" +
                                  "To show rejected                             : rejected \n" +
                                  "To show all                                  : all \n" +
                                  "To return back                               : back ");
                var choice = Console.ReadLine().ToLower();
                Console.Clear();
                
                if (string.Equals(choice, "all"))
                {
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
                            Console.WriteLine("\nID: "+ Convert.ToString(review.Id)+" || Title: " + review.Title + " | Content: " + review.Content + " || Star: " +
                                              review.Star + " || Status: " + review.Status + " || Operated by: " + review.OperatedBy);
                        }
                    }
                }
            }
        }

        public void ChangeStatus(List<Review>  reviews)
        {
            Console.WriteLine("\nPlease enter the title of the review:");
            var idRejected = Console.ReadLine();
            Console.Clear();
            
            foreach (var review in reviews)
            {
                if (string.Equals(idRejected, review.Title))
                {
                    Console.WriteLine("\nID: "+ review.Id +"\nTitle: " + review.Title + "\nContent: " + review.Content + "\nStar: " +
                                      review.Star + "\nStatus: " + review.Status + "\nOperated by: " + review.OperatedBy);
                    if (string.Equals(review.Status.ToLower(), "rejected"))
                    {
                        Console.WriteLine("Reject Reason: " + review.RejectReason);
                    }
                    Console.WriteLine("\nEnter new status:");
                    var newStat = Console.ReadLine();
                    Console.WriteLine("\nEnter reject reason (empty if not rejected):");
                    var rejectReason = Console.ReadLine();
                    review.Status = newStat;
                    review.RejectReason = rejectReason;
                    Console.Clear();
                }
            }
        }

        public void ChangeReview(List<Review>  reviews, string id)
        {
            Console.WriteLine("\nPlease enter the Title of the review:");
            var idChange = Console.ReadLine();
            Console.Clear();
            
            foreach (var review in reviews)
            {
                if (string.Equals(idChange, review.Title))
                {
                    if (string.Equals(Convert.ToString(review.OperatedBy), id))
                    {
                        Console.WriteLine("\nID: "+ review.Id +"\nTitle: " + review.Title + "\nContent: " + review.Content + "\nStar: " +
                                          review.Star + "\nStatus: " + review.Status + "\nOperated by: " + review.OperatedBy);
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
                            var choiceReview = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();
                            
                            switch (choiceReview)
                            {
                                case 1:
                                    Console.WriteLine("\nEnter new Title:");
                                    var newTitle = Console.ReadLine();
                                    review.Title = newTitle;
                                    changesDone = true;
                                    Console.Clear();
                                    break;
                                case 2:
                                    Console.WriteLine("\nEnter new Content:");
                                    var newContent = Console.ReadLine();
                                    review.Content = newContent;
                                    changesDone = true;
                                    Console.Clear();
                                    break;
                                case 3 :
                                    Console.WriteLine("\nEnter new Star:");
                                    var newStar = Convert.ToInt32(Console.ReadLine());
                                    review.Star = newStar;
                                    changesDone = true;
                                    Console.Clear();
                                    break;
                                
                                case 4:
                                    exitLoop = true;
                                    break;
                            }

                            if (changesDone)
                            {
                                review.Status = "pending";
                                review.RejectReason = null;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nThis comment does not belong to you");
                    }
                }
            }
        }

        public void CreateReview(List<Review>  reviews, string id)
        {
            
            var reviewId = new Guid();
            Console.WriteLine("\nEnter new Title:"); 
            var newTitle = Console.ReadLine(); 
            Console.WriteLine("\nEnter new Content:"); 
            var newContent = Console.ReadLine(); 
            Console.WriteLine("\nEnter new Star:"); 
            var newStar = Convert.ToInt32(Console.ReadLine());
            var review = new Review();
            review.Id = reviewId;
            review.Title = newTitle; 
            review.Content = newContent; 
            review.Star = newStar; 
            review.Status = "pending"; 
            review.OperatedBy = new Guid(id); 
            review.RejectReason = null;
            reviews.Add(review);
            Console.Clear();
        }
    }
}