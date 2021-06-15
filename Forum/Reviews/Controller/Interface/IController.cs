using System.Collections.Generic;
using Reviews.Models;

namespace Reviews.Controller.Interface
{
    public interface IController
    {
        void Observer(List<Admin> admins, List<User>  users, List<Review>  reviews);
        string Login(List<User>  users);
        void Register(List<User>  users);
        int LoginAdmin(List<Admin> admins);
        void CreateAdmin(List<Admin> admins);
        void ReadUserReview(List<Review>  reviews, string id);
        void ReadAllReview(List<Review>  reviews);
        void ReadAdminByStatus(List<Review>  reviews);
        void ChangeStatus(List<Review>  reviews);
        void ChangeReview(List<Review>  reviews, string id);
        void CreateReview(List<Review>  reviews, string id);
    }
}