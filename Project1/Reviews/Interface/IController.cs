using System;
using Reviews.Models;

namespace Reviews.Interface
{
    public interface IController
    {
        void Observer(Admin[] admins, User[] users, Review[] reviews, int userNumber, int reviewNumber);
        int Login(User[] users);
        void Register(User[] users, int userNumber);
        int LoginAdmin(Admin[] admins);
        void CreateAdmin(Admin[] admins);
        void ReadUserReview(Review[] reviews, int id);
        void ReadAllReview(Review[] reviews);
        void ReadAdminByStatus(Review[] reviews);
        void ChangeStatus(Review[] reviews);
        void ChangeReview(Review[] reviews, int id);
        void CreateReview(Review[] reviews, int id, int reviewNumber);
    }

    public class Controller : IController
    {
        public void Observer(Admin[] admins, User[] users, Review[] reviews, int userNumber, int reviewNumber)
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
                        while (id != 0)
                        {
                            Console.WriteLine("To see the reviews type: reviews" +
                                              "\nTo change a review type: change" +
                                              "\nTo create new review type: new" +
                                              "\nTo logout and return bak to main menu type: exit");
                            var userChoice = Console.ReadLine();
                            if (string.Equals(userChoice.ToLower(), "reviews"))
                            {
                                ReadUserReview(reviews, id);
                            }
                            else if (string.Equals(userChoice.ToLower(), "change"))
                            {
                                ChangeReview(reviews, id);
                            }
                            else if (string.Equals(userChoice.ToLower(), "new"))
                            {
                                CreateReview(reviews, id, reviewNumber);
                            }
                            else if (string.Equals(userChoice.ToLower(), "exit"))
                            {
                                id = 0;
                            }
                            
                        }
                        break;
                    case 2:
                        Register(users, userNumber);
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
                            var adminChoice = Console.ReadLine();
                            if (string.Equals(adminChoice.ToLower(), "reviews"))
                            {
                                ReadAdminByStatus(reviews);
                            }
                            else if (string.Equals(adminChoice.ToLower(), "change"))
                            {
                                ChangeStatus(reviews);
                            }
                            else if (string.Equals(adminChoice.ToLower(), "exit"))
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

        public int Login(User[] users)
        {
            Console.WriteLine("Please enter your E-mail:");
            var e = Console.ReadLine();
            Console.WriteLine("Please enter your Password:");
            var p = Console.ReadLine();
            foreach (var user in users)
            {
                if (string.Equals(user.email, e) )
                {
                    if (String.Equals(user.password, p))
                    {
                        return user.id;
                    }
                    else
                    {
                        Console.WriteLine("Wrong Password!");
                    }
                }
            }
            Console.WriteLine("Invalid user login attempt!\n Returning to main menu.");
            return 0;
        }

        public void Register(User[] users, int userNumber)
        {
            var id = Guid.NewGuid();
            users[userNumber].id = Convert.ToInt32(id);
            Console.WriteLine("Please enter your first name:");
            users[userNumber].firstName = Console.ReadLine();
            Console.WriteLine("Please enter your last name:");
            users[userNumber].lastName = Console.ReadLine();
            Console.WriteLine("Please enter your email:");
            users[userNumber].email = Console.ReadLine();
            Console.WriteLine("Please enter your password:");
            users[userNumber].password = Console.ReadLine();
            userNumber++;

        }

        public int LoginAdmin(Admin[] admins)
        {
            Console.WriteLine("Please enter your User Name:");
            var u = Console.ReadLine();
            Console.WriteLine("Please enter your Password:");
            var p = Console.ReadLine();
            foreach (var admin in admins)
            {
                if (string.Equals(admin.userName, u) )
                {
                    if (String.Equals(admin.password, p))
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

        public void CreateAdmin(Admin[] admins)
        {
            for (var i = 0; i < 2; i++)
            {
                var id = Guid.NewGuid();
                admins[i].id = Convert.ToInt32(id);
                admins[i].userName = "admin"+Convert.ToString(i+1);
                admins[i].password = "1234";
            }
        }

        public void ReadUserReview(Review[] reviews, int id)
        {
            var looper = true;
            while (looper)
            {
                Console.WriteLine("To show reviews type: reviews" +
                                  "\nTo return back type: back");
                var choice = Console.ReadLine();
                
                if (string.Equals(choice.ToLower(), "back"))
                {
                    looper = false;
                }
                else if(string.Equals(choice.ToLower(), "reviews"))
                {
                    foreach (var review in reviews)
                    {
                        if (review.operatedBy == id)
                        {
                            Console.WriteLine("ID: "+ review.id+"Title: " + review.title + "|| Content: " + review.content + "|| Star: " +
                                              review.star + "|| Status: " + review.status + "|| Operated by: " + review.operatedBy);
                        }
                    }
                }
            }
        }

        public void ReadAllReview(Review[] reviews)
        {
            var looper = true;
            while (looper)
            {
                Console.WriteLine("To show reviews type: reviews" +
                                  "\nTo return back type: back");
                var choice = Console.ReadLine();
                
                if (string.Equals(choice.ToLower(), "back"))
                {
                    looper = false;
                }
                else if(string.Equals(choice.ToLower(), "reviews"))
                {
                    foreach (var review in reviews)
                    {
                        if (String.Equals(review.status.ToLower(), "approved"))
                        {
                            Console.WriteLine("ID: "+ review.id+"Title: " + review.title + "|| Content: " + review.content + "|| Star: " +
                                              review.star + "|| Status: " + review.status + "|| Operated by: " + review.operatedBy);
                        }
                    }
                }
            }
        }

        public void ReadAdminByStatus(Review[] reviews)
        {
            var looper = true;
            while (looper)
            {
                Console.WriteLine("To show approved or pending or rejected or all to the whole word: " +
                                  "\nTo return back type back: ");
                var choice = Console.ReadLine();
                
                if (string.Equals(choice.ToLower(), "all"))
                {
                    foreach (var review in reviews)
                    {
                        Console.WriteLine("ID: "+ review.id+"Title: " + review.title + "|| Content: " + review.content + "|| Star: " +
                                          review.star + "|| Status: " + review.status + "|| Operated by: " + review.operatedBy);
                    }
                }
                else if (string.Equals(choice.ToLower(), "back"))
                {
                    looper = false;
                }
                else
                {
                    foreach (var review in reviews)
                    {
                        if (String.Equals(review.status.ToLower(), choice.ToLower()))
                        {
                            Console.WriteLine("ID: "+ review.id+"Title: " + review.title + "|| Content: " + review.content + "|| Star: " +
                                              review.star + "|| Status: " + review.status + "|| Operated by: " + review.operatedBy);
                        }
                    }
                }
            }
        }

        public void ChangeStatus(Review[] reviews)
        {
            Console.WriteLine("Please enter the id of the review:");
            var idRejected = Convert.ToInt32(Console.ReadLine());
            foreach (var review in reviews)
            {
                if (idRejected == review.id)
                {
                    Console.WriteLine("ID: "+ idRejected +"\nTitle: " + review.title + "\nContent: " + review.content + "\nStar: " +
                                      review.star + "\nStatus: " + review.status + "\nOperated by: " + review.operatedBy);
                    if (string.Equals(review.status.ToLower(), "rejected"))
                    {
                        Console.WriteLine("Reject Reason: " + review.rejectReason);
                    }
                    Console.WriteLine("Enter new status:");
                    var newStat = Console.ReadLine();
                    Console.WriteLine("Enter reject reason (empty if not rejected):");
                    var rejectReason = Console.ReadLine();
                    review.status = newStat;
                    review.rejectReason = rejectReason;
                }
            }
        }

        public void ChangeReview(Review[] reviews, int id)
        {
            Console.WriteLine("Please enter the id of the review:");
            var idChange = Convert.ToInt32(Console.ReadLine());
            foreach (var review in reviews)
            {
                if (idChange == review.id)
                {
                    if (review.operatedBy == id)
                    {
                        Console.WriteLine("ID: "+ idChange +"\nTitle: " + review.title + "\nContent: " + review.content + "\nStar: " +
                                          review.star + "\nStatus: " + review.status + "\nOperated by: " + review.operatedBy);
                        if (string.Equals(review.status.ToLower(), "rejected"))
                        {
                            Console.WriteLine("Reject Reason: " + review.rejectReason);
                        }
                        Console.WriteLine("Enter new Title:");
                        var newTitle = Console.ReadLine();
                        Console.WriteLine("Enter new Content:");
                        var newContent = Console.ReadLine();
                        Console.WriteLine("Enter new Star:");
                        var newStar = Console.ReadLine();
                        review.title = newTitle;
                        review.content = newContent;
                        review.star = newStar;
                        review.status = "pending";
                        review.rejectReason = null;
                    }
                    else
                    {
                        Console.WriteLine("This comment does not belong to you");
                    }
                    
                }
            }
        }

        public void CreateReview(Review[] reviews, int id, int reviewNumber)
        {
            Console.WriteLine("Enter new Title:"); 
            var newTitle = Console.ReadLine(); 
            Console.WriteLine("Enter new Content:"); 
            var newContent = Console.ReadLine(); 
            Console.WriteLine("Enter new Star:"); 
            var newStar = Console.ReadLine(); 
            reviews[reviewNumber].title = newTitle; 
            reviews[reviewNumber].content = newContent; 
            reviews[reviewNumber].star = newStar; 
            reviews[reviewNumber].status = "pending"; 
            reviews[reviewNumber].operatedBy = id; 
            reviews[reviewNumber].rejectReason = null;

            reviewNumber++;
        }
    }
}