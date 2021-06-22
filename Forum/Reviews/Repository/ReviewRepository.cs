using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Reviews.Models;
using Reviews.Mongo;
using Reviews.Repository.Interface;

namespace Reviews.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly Repository<Review> _repositoryReview;

        public ReviewRepository(Repository<Review> repositoryReview)
        {
            _repositoryReview = repositoryReview;
        }
        
        public void ReadReviews(List<Review> reviews)
        {
            var filter = Builders<Review>.Filter.Empty;
            var data = _repositoryReview.ReadRecord(filter);
            reviews.AddRange(data.Select(review => new Review
            {
                Id = review.Id,
                Content = review.Content,
                Title = review.Title,
                Star = review.Star,
                Status = review.Status,
                RejectReason = review.RejectReason,
                OperatedBy = review.OperatedBy
            }));
        }

        public void WriteReview(Review review)
        {
            _repositoryReview.CreateRecord(review);
        }
        //dependency injection for console application

        public void UpdateReview(Review review)
        {
            var filter = Builders<Review>.Filter.Eq("_id", review.Id);
            var update = Builders<Review>.Update
                .Set("Title", review.Title)
                .Set("Content", review.Content)
                .Set("Star", review.Star)
                .Set("Status", review.Status)
                .Set("RejectReason", review.RejectReason);
            _repositoryReview.UpdateRecord(filter, update) ;
        }
    }
}