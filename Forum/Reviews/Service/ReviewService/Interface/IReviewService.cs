using System.Collections.Generic;
using Reviews.Models;
using Reviews.Mongo;

namespace Reviews.Service.ReviewService.Interface
{
    public interface IReviewService
    {
        void ReadUserReview(List<Review> reviews, string id);
        void ReadAllReview(List<Review> reviews);
        void ReadAdminByStatus(List<Review> reviews);
        void CreateReview(List<Review> reviews, string id, Repository<Review> repositoryReview);
        void ChangeStatus(IEnumerable<Review> reviews, Repository<Review> repositoryReview);
        void ChangeReview(List<Review> reviews, string id, Repository<Review> repositoryReview);
    }
}