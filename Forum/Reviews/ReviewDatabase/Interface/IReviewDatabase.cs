using System.Collections.Generic;
using Reviews.Models;

namespace Reviews.ReviewDatabase.Interface
{
    public interface IReviewDatabase
    {
        void ReadReviews(List<Review> reviews);
        void WriteReview(Review review);
        void UpdateReview(Review review);
    }
}