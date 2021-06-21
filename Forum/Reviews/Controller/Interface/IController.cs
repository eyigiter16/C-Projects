using System.Collections.Generic;
using Reviews.Models;
using Reviews.Mongo;

namespace Reviews.Controller.Interface
{
    public interface IController
    {
        void Observer(List<Admin> admins, List<User>  users, List<Review>  reviews, Repository<User> repositoryUser, Repository<Review> repositoryReview);
        string Login(List<User>  users);
        void Register(List<User>  users, Repository<User> repositoryUser);
        int LoginAdmin(IEnumerable<Admin> admins);
        void CreateAdmin(List<Admin> admins);
        void ReadUserReview(List<Review>  reviews, string id);
        void ReadAllReview(List<Review>  reviews);
        void ReadAdminByStatus(List<Review>  reviews);
        void ChangeStatus(List<Review>  reviews, Repository<Review> repositoryReview);
        void ChangeReview(List<Review>  reviews, string id, Repository<Review> repositoryReview);
        void CreateReview(List<Review>  reviews, string id, Repository<Review> repositoryReview);
    }
}