using System;
using System.Collections.Generic;
using Reviews.Controller.Interface;
using Reviews.Models;
using Reviews.Mongo;

namespace Reviews.Controller
{
    public class Controller : IController
    {
        public void Observer(List<Admin> admins, List<User>  users, List<Review>  reviews, Repository<User> repositoryUser, Repository<Review> repositoryReview)
        {
            var revOps = new ReviewOperations.ReviewOperations();
            var userOps = new UserOperations.UserOperations();
            var adminOps = new AdminOperations.AdminOperations();
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
                    var id = userOps.Login(users);
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
                            revOps.ReadUserReview(reviews, id);
                        }
                        else if (string.Equals(userChoice, "2"))
                        {
                            revOps.ChangeReview(reviews, id, repositoryReview);
                        }
                        else if (string.Equals(userChoice, "3"))
                        {
                            revOps.CreateReview(reviews, id, repositoryReview);
                        }
                        else if (string.Equals(userChoice, "4"))
                        { 
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
                else if (string.Equals(choice, "2"))
                { 
                    userOps.Register(users, repositoryUser);
                }
                else if (string.Equals(choice, "3"))
                {
                    revOps.ReadAllReview(reviews);
                }
                else if (string.Equals(choice, "4"))
                {
                    var checkAdmin = adminOps.LoginAdmin(admins);
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
                            revOps.ReadAdminByStatus(reviews);
                        }
                        else if (string.Equals(adminChoice, "2"))
                        {
                            revOps.ChangeStatus(reviews, repositoryReview);
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
                                      "To exit                                      : 1 \n" +
                                      "To make a new choice                         : (just enter) ");
                    var again = Console.ReadLine()?.ToLower();
                    Console.Clear();

                    if (!string.Equals(again, "1")) continue;
                    Console.WriteLine("\nExiting. ");
                    looper = false;
                }
            }

        }
    }
}