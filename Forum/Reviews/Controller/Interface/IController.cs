using System.Collections.Generic;
using Reviews.Models;
using Reviews.Mongo;

namespace Reviews.Controller.Interface
{
    public interface IController
    {
        void Observer(List<Admin> admins, List<User>  users, List<Review>  reviews, Repository<User> repositoryUser, Repository<Review> repositoryReview);
    }
}