using System;
using System.Collections.Generic;
using System.Data;

namespace ClassLibrary1
{
    public class ReviewSystem
    {
        private static Dictionary<string, Dictionary<string, string>> admins =
            new Dictionary<string, Dictionary<string, string>>();

        private static Dictionary<string, Dictionary<string, string>> user =
            new Dictionary<string, Dictionary<string, string>>();

        private static Dictionary<string, Dictionary<string, string>> reviews =
            new Dictionary<string, Dictionary<string, string>>();

        private static int numOfUsers = 0;

        private static int numOfReviews = 0;

        public static void Main()
        {
            for (var i = 1; i < 3; i++)
            {
                var adminInfo = new Dictionary<string, string>();
                adminInfo.Add("id", Convert.ToString(i));
                adminInfo.Add("username", "admin" + Convert.ToString(i));
                adminInfo.Add("password", "1234");
                admins.Add(Convert.ToString(i), adminInfo);
            }

            Console.WriteLine(
                "For User Login type: 1\nFor Registration type: 2\nTo show reviews type: 3\nFor Admin Login type: 4");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Please enter your email:");
                    var mail = Console.ReadLine();
                    Console.WriteLine("Please enter your Password:");
                    var password = Console.ReadLine();
                    if (!Login(mail, password))
                    {
                        Console.WriteLine("Login unsuccessful!\nGood Bye!");
                    }
                    else
                    {
                        var a = true;
                        while (a)
                        {
                            Console.WriteLine(
                                "To show all the approved reviews: 1\nTo show your reviews: 2\nTo edit your rejected reviews: 3\nCreate a new review: 4\nTo exit: 5");
                            var userChoice = Convert.ToInt32(Console.ReadLine());
                            switch (userChoice)
                            {
                                case 1:
                                    ReadReviews();
                                    break;
                                case 2:
                                    foreach (var item in reviews)
                                    {
                                        if (String.Equals(item.Value["id"], user[mail]["id"]))
                                        {
                                            Console.WriteLine("ID: "+ item.Key+"Title: " + item.Value["title"] + "|| Content: " +
                                                              item.Value["content"] + "|| Star: " + item.Value["star"] +
                                                              "|| Status: " + item.Value["status"] + "|| Operated by: " +
                                                              item.Value["operatedBy"]);
                                            if (string.Equals(item.Value["status"], "rejected"))
                                            {
                                                Console.WriteLine("Reject Reason: " + item.Value["rejectReason"]);
                                            }
                                        }
                                    }
                                    break;
                                case 3:
                                    Console.WriteLine("Please enter the id of the review:");
                                    var idRejected = Console.ReadLine();
                                    UpdateReview(idRejected);
                                    break;
                                case 4:
                                    CreateReview(user[mail]["id"]);
                                    break;
                                case 5:
                                    a = false;
                                    break;
                            }
                        }
                    }
                    break;
                case 2:
                    mail = Create();
                    Console.WriteLine("Would you like to write a comment: y | n");
                    var answer1 = Console.ReadLine();
                    if (string.Equals(answer1, "y"))
                    {
                        CreateReview(user[mail]["id"]);
                    }
                    else if (string.Equals(answer1, "n"))
                    {
                        Console.WriteLine("Would you like to read the comments: y | n");
                        var answer2 = Console.ReadLine();
                        if (string.Equals(answer2, "y"))
                        {
                            ReadReviews();
                        }
                    }

                    break;
                case 3:
                    ReadReviews();
                    break;
                case 4:
                    Console.WriteLine("Please enter your Admin id:");
                    var aid = Console.ReadLine();
                    if (AdminLogin(aid))
                    {
                        Console.WriteLine("Show reviews: 1\nApprove/Reject reviews: 2");
                        int adminChoice = Convert.ToInt32(Console.ReadLine());
                        if (adminChoice == 1)
                        {
                            Console.WriteLine("Show approved or pending or rejected or all (write in lowercase)");
                            string adminChoice2 = Console.ReadLine();
                            ReadReviews(adminChoice2);
                        }
                        else if (adminChoice == 2)
                        {
                            Console.WriteLine("Input the ID of the review.");
                            var id = Console.ReadLine();
                            Console.WriteLine("ID: "+ id +"Title: " + reviews[id]["title"] + "|| Content: " + reviews[id]["content"] + "|| Star: " +
                                              reviews[id]["star"] + "|| Status: " + reviews[id]["status"] + "|| Operated by: " +
                                              reviews[id]["operatedBy"]+ "Reject Reason: " + reviews[id]["rejectReason"]);
                            Console.WriteLine("Enter new star:");
                            var newStar = Console.ReadLine();
                            Console.WriteLine("Enter new status:");
                            var newStatus = Console.ReadLine();
                            Console.WriteLine("Enter reject reason:");
                            var newRejectReason = Console.ReadLine();
                            reviews[id]["star"] = newStar;
                            reviews[id]["status"] = newStatus;
                            reviews[id]["operatedBy"] = admins[aid]["username"];
                            reviews[id]["rejectReason"] = newRejectReason;
                        }
                    }
                    break;
            }
        }

        private static Boolean AdminLogin(string id)
        {
            Console.WriteLine("Please enter your Password:");
            var p = Console.ReadLine();
            if (admins.ContainsKey(id))
            {
                if (String.Equals(admins[id]["password"], p))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Wrong Password!");
                }
            }
            else
            {
                Console.WriteLine("Wrong Admin ID!");
            }
            return false;
        }
        private static Boolean Login(string e, string p)
        {
            if (user.ContainsKey(e))
            {
                if (String.Equals(user[e]["password"], p))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Wrong Password!");
                }
            }
            else
            {
                Console.WriteLine("Email does not exist in the system.");
            }

            return false;
        }

        private static string Create()
        {
            numOfUsers++;
            var miniUser = new Dictionary<string, string>();
            miniUser.Add("id", Convert.ToString(numOfUsers));
            Console.WriteLine("Please enter your first name:");
            miniUser.Add("firstName", Console.ReadLine());
            Console.WriteLine("Please enter your last name:");
            miniUser.Add("lastName", Console.ReadLine());
            Console.WriteLine("Please enter your email:");
            miniUser.Add("email", Console.ReadLine());
            Console.WriteLine("Please enter your password:");
            miniUser.Add("password", Console.ReadLine());
            user.Add(miniUser["email"], miniUser);
            return miniUser["email"];
        }
        
        private static void CreateReview(string userId)
        {
            numOfReviews++;
            var review = new Dictionary<string, string>();
            review.Add("id",userId);
            Console.WriteLine("Please enter the title:");
            review.Add("title", Console.ReadLine());
            Console.WriteLine("Please enter the content:");
            review.Add("content", Console.ReadLine());
            review.Add("star", "0");
            review.Add("status", "pending");
            review.Add("rejectReason", null);
            review.Add("operatedBy", null);
            reviews.Add(Convert.ToString(numOfReviews),review);
        }

        private static void UpdateReview(string id)
        {
            Console.WriteLine("ID: "+ id +"Title: " + reviews[id]["title"] + "|| Content: " + reviews[id]["content"] + "|| Star: " +
                              reviews[id]["star"] + "|| Status: " + reviews[id]["status"] + "|| Operated by: " +
                              reviews[id]["operatedBy"]+ "Reject Reason: " + reviews[id]["rejectReason"]);
            Console.WriteLine("Enter new title:");
            var newTitle = Console.ReadLine();
            reviews[id]["title"] = newTitle;
            Console.WriteLine("Enter new content:");
            var newContent = Console.ReadLine();
            reviews[id]["content"] = newContent;
            reviews[id]["star"] = "0";
            reviews[id]["status"] = "pending"; 
            reviews[id]["operatedBy"] = null;
            reviews[id]["rejectReason"] = null;
        }

        private static void ReadReviews(string stat = "approved")
        {
            if (string.Equals(stat, "approved"))
            {
                foreach (var item in reviews)
                {
                    if (String.Equals(item.Value["status"], "approved"))
                    {
                        Console.WriteLine("ID: "+ item.Key+"Title: " + item.Value["title"] + "|| Content: " + item.Value["content"] + "|| Star: " +
                                          item.Value["star"] + "|| Status: " + item.Value["status"] + "|| Operated by: " +
                                          item.Value["operatedBy"]);
                    }
                }
            }
            else if (string.Equals(stat, "pending"))
            {
                foreach (var item in reviews)
                {
                    if (String.Equals(item.Value["status"], "pending"))
                    {
                        Console.WriteLine("ID: "+ item.Key+"Title: " + item.Value["title"] + "|| Content: " + item.Value["content"] + "|| Status: " +
                                          item.Value["status"]);
                    }
                }
            }
            else if (string.Equals(stat, "rejected"))
            {
                foreach (var item in reviews)
                {
                    if (String.Equals(item.Value["status"], "rejected"))
                    {
                        Console.WriteLine("ID: "+ item.Key+"Title: " + item.Value["title"] + "|| Content: " + item.Value["content"] + "|| Star: " +
                                          item.Value["star"] + "|| Status: " + item.Value["status"] + "|| Operated by: " +
                                          item.Value["operatedBy"] + "Reject Reason: " + item.Value["rejectReason"]);
                    }
                }
            }
            else if (string.Equals(stat, "all"))
            {
                foreach (var item in reviews)
                {
                    Console.WriteLine("ID: "+ item.Key+"Title: " + item.Value["title"] + "|| Content: " + item.Value["content"] + "|| Star: " +
                                      item.Value["star"] + "|| Status: " + item.Value["status"] + "|| Operated by: " +
                                      item.Value["operatedBy"] + "Reject Reason: " + item.Value["rejectReason"]);
                }

            }
        }
    }
}