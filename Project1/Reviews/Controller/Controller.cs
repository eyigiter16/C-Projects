using System;
using System.Collections.Generic;
using System.Linq;
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
                Console.WriteLine("\nTo User Login                                : 1\n" +
                                  "To Registration                              : 2\n" +
                                  "To show reviews                              : 3\n" +
                                  "To Admin Login                               : 4\n" +
                                  "To exit                                      : 5");
                var choice = Console.ReadLine();
                Console.Clear();
                if (string.Equals(choice, "1"))
                {
                    var id = Login(users);
                        var mistakeCounter = 0;
                        while (id != null)
                        {
                            Console.WriteLine("\nTo see the reviews                           : 1\n" +
                                              "To change a review                           : 2\n" +
                                              "To create new review                         : 3\n" +
                                              "To logout and return back to main menu       : 4");
                            var userChoice = Console.ReadLine()?.ToLower();
                            Console.Clear();
                            
                            if (string.Equals(userChoice, "1"))
                            {
                                ReadUserReview(reviews, id);
                            }
                            else if (string.Equals(userChoice, "2"))
                            {
                                ChangeReview(reviews, id);
                            }
                            else if (string.Equals(userChoice, "3"))
                            {
                                CreateReview(reviews, id);
                            }
                            else if (string.Equals(userChoice, "4"))
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
                                                      "If you give another invalid input you \n" +
                                                      "will be sent back to main menu. \n" +
                                                      "To logout and return back to main menu       : 1 \n" +
                                                      "To make a new choice                         : 2 ");
                                    var mistakeChoice = Console.ReadLine()?.ToLower();
                                    Console.Clear();
                                    
                                    if (string.Equals(mistakeChoice, "1"))
                                    {
                                        
                                        Console.WriteLine("\nReturning back to main menu. ");
                                        id = null;
                                    }
                                    else if (!string.Equals(mistakeChoice, "2"))
                                    {
                                        Console.WriteLine("\nInvalid input again! \n" +
                                                          "Returning back to main menu. ");
                                        id = null;
                                    }
                                }
                            }
                        }
                }
                else if (string.Equals(choice, "2"))
                { 
                    Register(users);
                }
                else if (string.Equals(choice, "3"))
                {
                    ReadAllReview(reviews);
                }
                else if (string.Equals(choice, "4"))
                {
                    var checkAdmin = LoginAdmin(admins);
                    var mistakeCounter1 = 0;
                    while (checkAdmin == 1)
                    {
                        Console.WriteLine("\nTo see the reviews                           : 1\n" +
                                          "To change status of reviews                  : 2\n" +
                                          "To logout and return back to main menu       : 3");
                        var adminChoice = Console.ReadLine()?.ToLower();
                        Console.Clear();

                        if (string.Equals(adminChoice, "1"))
                        {
                            ReadAdminByStatus(reviews);
                        }
                        else if (string.Equals(adminChoice, "2"))
                        {
                            ChangeStatus(reviews);
                        }
                        else if (string.Equals(adminChoice, "3"))
                        {
                            checkAdmin = 0;
                        }
                        else
                        {
                            mistakeCounter1++;
                            if (mistakeCounter1 == 2)
                            {

                                Console.WriteLine("\nInvalid input again! \n" +
                                                  "Returning back to main menu. ");
                                checkAdmin = 0;
                            }
                            else
                            {
                                Console.WriteLine("\nInvalid input! \n" +
                                                  "If you give another invalid input you \n" +
                                                  "will be sent back to main menu. \n" +
                                                  "To logout and return back to main menu       : 1 \n" +
                                                  "To make a new choice                         : 2 ");
                                var mistakeChoice = Console.ReadLine()?.ToLower();
                                Console.Clear();

                                if (string.Equals(mistakeChoice, "1"))
                                {

                                    Console.WriteLine("\nReturning back to main menu. ");
                                    checkAdmin = 0;
                                }
                                else if (!string.Equals(mistakeChoice, "2"))
                                {

                                    Console.WriteLine("\nInvalid input again! \n" +
                                                      "Returning back to main menu. ");
                                    checkAdmin = 0;
                                }
                            }
                        }
                    }
                }
                else if (string.Equals(choice, "5"))
                {
                    Console.WriteLine("\nExiting. ");
                    looper = false;
                }
                else
                {
                    Console.WriteLine("\nInvalid input! \n" +
                                      "To logout and return back to main menu       : 1 \n" +
                                      "To make a new choice                         : (just enter) ");
                    var again = Console.ReadLine()?.ToLower();
                    Console.Clear();

                    if (!string.Equals(again, "1")) continue;
                    Console.WriteLine("\nExiting. ");
                    looper = false;
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
            
            foreach (var user in users.Where(user => string.Equals(user.Email, e)))
            {
                if (string.Equals(user.Password, p))
                {
                    return Convert.ToString(user.Id);
                }
                Console.WriteLine("\nWrong Password! ");
            }
            
            Console.WriteLine("\nInvalid user login attempt! \nReturning to main menu. ");
            return null;
        }

        public void Register(List<User> users)
        {
            var id = Guid.NewGuid();
            var user = new User {Id = id};
            Console.WriteLine("\nPlease enter your first name: ");
            user.FirstName = Console.ReadLine();
            Console.WriteLine("\nPlease enter your last name: ");
            user.LastName = Console.ReadLine();
            Console.WriteLine("\nPlease enter your email: ");
            user.Email = Console.ReadLine();
            var matches = users.Where(user1 => string.Equals(user1.Email, user.Email));
            while(true)
            {
                if (!matches.Any())
                {
                    break;
                }
                Console.WriteLine("This email is already on the use by another user." +
                                  "\nPlease provide a new email.");
                Console.WriteLine("\nPlease enter your email: ");
                user.Email = Console.ReadLine();
                matches = users.Where(user1 => string.Equals(user1.Email, user.Email));
            }
            
            Console.WriteLine("\nPlease enter your password: ");
            user.Password = Console.ReadLine();
            users.Add(user);
            Console.Clear();
            new ReadWrite.ReadWrite().WriteUser(users);
        }

        public int LoginAdmin(List<Admin> admins)
        {
            Console.WriteLine("\nPlease enter your User Name:");
            var u = Console.ReadLine();
            Console.WriteLine("\nPlease enter your Password:");
            var p = Console.ReadLine();
            Console.Clear();
            
            foreach (var admin in admins.Where(admin => string.Equals(admin.UserName, u)))
            {
                if (string.Equals(admin.Password, p))
                {
                    return 1;
                }
                Console.WriteLine("\nWrong Password! ");
            }
            Console.WriteLine("\nInvalid admin login attempt!\n Returning to main menu.");
            return 0;
        }

        public void CreateAdmin(List<Admin> admins)
        {
            for (var i = 0; i < 2; i++)
            {
                var id = Guid.NewGuid();
                var admin = new Admin {Id = id, UserName = "admin" + Convert.ToString(i + 1), Password = "1234"};
                admins.Add(admin);
            }
        }

        public void ReadUserReview(List<Review>  reviews, string id)
        {
            var looper = true;
            while (looper)
            {
                Console.WriteLine("\nTo show reviews                              : 1 " +
                                  "\nTo return back                               : 2 ");
                var choice = Console.ReadLine()?.ToLower();
                Console.Clear();

                if (string.Equals(choice, "2"))
                {
                    looper = false;
                }
                else if(string.Equals(choice, "1"))
                {
                    var matches = reviews.Where(review => string.Equals(Convert.ToString(review.OperatedBy), id));
                    var matches1 = reviews.Where(review => string.Equals(review.Status, "approved"));
                    if (!matches.Any() && !matches1.Any())
                    {
                        Console.WriteLine("\nNo reviews exist yet. ");
                        continue;
                    }
                    foreach (var review in reviews.Where(review => string.Equals(Convert.ToString(review.OperatedBy), id)))
                    {
                        Console.WriteLine("\nID: "+ Convert.ToString(review.Id)+" || Title: " + review.Title + " || Content: " + review.Content + " || Star: " +
                                          review.Star + " || Status: " + review.Status + " || Operated by: " + review.OperatedBy);
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
            while (looper)
            {
                Console.WriteLine("\nTo show reviews                              : 1 " +
                                  "\nTo return back                               : 2 ");
                var choice = Console.ReadLine()?.ToLower();
                Console.Clear();
                
                if (string.Equals(choice, "2"))
                {
                    looper = false;
                }
                else if(string.Equals(choice, "1"))
                {
                    var matches = reviews.Where(review => string.Equals(review.Status, "approved"));
                    if (!matches.Any())
                    {
                        Console.WriteLine("\nNo reviews exist yet. ");
                        continue;
                    }
                    foreach (var review in reviews.Where(review => string.Equals(review.Status, "approved")))
                    {
                        Console.WriteLine("\nID: "+ Convert.ToString(review.Id)+" || Title: " + review.Title + " || Content: " + review.Content + " || Star: " +
                                          review.Star + " || Status: " + review.Status + " || Operated by: " + review.OperatedBy);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! \n");
                }
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

        public void ChangeStatus(List<Review>  reviews)
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
                new ReadWrite.ReadWrite().WriteReview(reviews);
            }
        }

        public void ChangeReview(List<Review>  reviews, string id)
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
                                    if (!string.Equals(newStar, "1") && !string.Equals(newStar, "2") &&
                                        !string.Equals(newStar, "3") &&
                                        !string.Equals(newStar, "4") && !string.Equals(newStar, "5"))
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
                            new ReadWrite.ReadWrite().WriteReview(reviews);
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

        public void CreateReview(List<Review>  reviews, string id)
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
                if (!string.Equals(newStar, "1") && !string.Equals(newStar, "2") && !string.Equals(newStar, "3") &&
                    !string.Equals(newStar, "4") && !string.Equals(newStar, "5"))
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
            new ReadWrite.ReadWrite().WriteReview(reviews);
        }
    }
}