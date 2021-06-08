using System;
using System.IO;
using Reviews.Models;

namespace Reviews.Interface
{
    public interface IReadWrite
    {
        int ReadUser(User[] users);
        int ReadReviws(Review[] reviews);
        void Write(User[] users, Review[] reviews);
    }

    public class ReadWrite : IReadWrite
    {
        public int ReadUser(User[] users)
        {
            var userNumber = 0;
            using(var file = new StreamReader("users.txt"))
            {
                string ln;
                while ((ln = file.ReadLine()) != null) {  
                    string[] tokens = ln.Split(' ');
                    users[userNumber].id = Convert.ToInt32(tokens[0]);
                    users[userNumber].firstName = tokens[1];
                    users[userNumber].lastName = tokens[2];
                    users[userNumber].email = tokens[3];
                    users[userNumber].password = tokens[4];
                    userNumber++;
                }  
                file.Close();
            }

            return userNumber;
        }

        public int ReadReviws(Review[] reviews)
        {
            var reviewNumber = 0;
            using(var file = new StreamReader("reviews.txt"))
            {
                string ln;
                while ((ln = file.ReadLine()) != null) {  
                    string[] tokens = ln.Split('|');
                    reviews[reviewNumber].id = Convert.ToInt32(tokens[0]);
                    reviews[reviewNumber].content = tokens[1];
                    reviews[reviewNumber].title = tokens[2];
                    reviews[reviewNumber].star = tokens[3];
                    reviews[reviewNumber].status = tokens[4];
                    reviews[reviewNumber].rejectReason = tokens[5];
                    reviews[reviewNumber].operatedBy = Convert.ToInt32(tokens[6]);
                    reviewNumber++;
                }  
                file.Close();
            }
            
            return reviewNumber;
        }
        
        public void Write(User[] users, Review[] reviews)
        {
            foreach (var currentUser in users)
            {
                using(StreamWriter writetext = new StreamWriter("users.txt"))
                {
                    writetext.WriteLine(currentUser.id +" "+ currentUser.firstName +" "+
                                        currentUser.lastName +" "+ currentUser.email+
                                        currentUser.password);
                }
            }
            foreach (var currentReview in reviews)
            {
                using(StreamWriter writetext = new StreamWriter("reviews.txt"))
                {
                    writetext.WriteLine(currentReview.id +"|"+ currentReview.content +"|"+
                                        currentReview.title +"|"+ currentReview.star +"|"+
                                        currentReview.status +"|"+ currentReview.rejectReason
                                        +"|"+ currentReview.operatedBy);
                }
            }
        }
    }
}