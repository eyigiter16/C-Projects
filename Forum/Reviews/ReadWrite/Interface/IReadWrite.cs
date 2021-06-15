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
        void WriteUser(List<User> users);
        void WriteReview(List<Review> reviews);
    }
}