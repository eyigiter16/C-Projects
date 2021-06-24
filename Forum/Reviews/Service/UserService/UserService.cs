using System;
using System.Collections.Generic;
using System.Linq;
using Reviews.Exception;
using Reviews.Models;
using Reviews.Mongo;
using Reviews.Repository;
using Reviews.Service.UserService.Interface;

namespace Reviews.Service.UserService
{
    public class UserService : IUserService
    {
         public string Login(IEnumerable<User> users)
        {
            Console.WriteLine("\nTo return back                                   : 1 " +
                              "\nPlease enter your E-mail: ");
            var c = Console.ReadLine();
            if (string.Equals(c, "1"))
            {
                Console.Clear();
                Console.WriteLine("\nTo return back                               : 1 " +
                                  "\nReturning to main menu. ");
                return null;
            }
            Console.WriteLine("\nPlease enter your Password: ");
            var p = Console.ReadLine();
            Console.Clear();
            if (string.Equals(p, "1"))
            {
                Console.WriteLine("\nReturning to main menu. ");
                return null;
            }
            
            foreach (var user in users.Where(user => string.Equals(user.Email, c)))
            {
                if (BCrypt.Net.BCrypt.Verify(p, user.Password))
                {
                    return Convert.ToString(user.Id);
                }
                Console.WriteLine("\nWrong Password! ");
            }
            try
            {
                throw new InvalidLogin();
            }
            catch (InvalidLogin e)
            {
                Console.WriteLine(e.Code+e.Message);
            }
            return null;
        }

        public void Register(List<User> users, Repository<User> repositoryUser)
        {
            var id = Guid.NewGuid();
            var user = new User {Id = id};
            Console.WriteLine("\nPlease enter your first name: ");
            user.FirstName = Console.ReadLine();
            Console.WriteLine("\nPlease enter your last name: ");
            user.LastName = Console.ReadLine();
            Console.WriteLine("\nPlease enter your email: ");
            user.Email = Console.ReadLine();
            var matches = users.Where(user1 => string.Equals(user1.Email, user.Email));
            while(true)
            {
                if (!matches.Any())
                {
                    break;
                }
                Console.WriteLine("This email is already on the use by another user." +
                                  "\nPlease provide a new email.");
                Console.WriteLine("\nPlease enter your email: ");
                user.Email = Console.ReadLine();
                matches = users.Where(user1 => string.Equals(user1.Email, user.Email));
            }
            Console.WriteLine("\nPlease enter your password: (Must be min 8 characters)");
            user.Password = Console.ReadLine();
            while (true)
            {
                if (string.Equals(user.Password, "1"))
                {
                    Console.Clear();
                    Console.WriteLine("\nReturning to main menu. ");
                    return;
                }
                if (user.Password?.Length < 8)
                {
                    Console.WriteLine("Password must be at least8 characters." +
                                      "\nPlease provide a new password.");
                    Console.WriteLine("\nPlease enter your password: (Must be min 8 characters)");
                    user.Password = Console.ReadLine();
                }
                else
                {
                    break;   
                }
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            users.Add(user);
            Console.Clear();
            new UserRepository(repositoryUser).WriteUser(user);
        }
        
    }
}