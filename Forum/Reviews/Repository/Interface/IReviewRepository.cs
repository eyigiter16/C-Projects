using System.Collections.Generic;
using Reviews.Models;

namespace Reviews.Repository.Interface
{
    public interface IReviewRepository
    {
        void ReadReviews(List<Review> reviews);
        void WriteReview(Review review);
        void UpdateReview(Review review);
    }
}