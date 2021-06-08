using System;
using System.Collections.Generic;
using System.IO;
using Reviews.Models;

namespace Reviews.ReadWrite.Interface
{
    public interface IReadWrite
    {
        void ReadUser(List<User> users);
        void ReadReviews(List<Review> reviews);
        void Write(List<User> users, List<Review> reviews);
    }
}