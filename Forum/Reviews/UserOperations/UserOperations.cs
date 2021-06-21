using System;
using System.Collections.Generic;
using System.Linq;
using Reviews.Models;
using Reviews.Mongo;
using Reviews.UserOperations.Interface;

namespace Reviews.UserOperations
{
    public class UserOperations : IUserOperations
    {
         public string Login(IEnumerable<User> users)
        {
            Console.WriteLine("\nPlease enter your E-mail: ");
            var e = Console.ReadLine();
            Console.WriteLine("\nPlease enter your Password: ");
            var p = Console.ReadLine();
            Console.Clear();
            
            foreach (var user in users.Where(user => string.Equals(user.Email, e)))
            {
                if (BCrypt.Net.BCrypt.Verify(p, user.Password))
                {
                    return Convert.ToString(user.Id);
                }
                Console.WriteLine("\nWrong Password! ");
            }
            
            Console.WriteLine("\nInvalid user login attempt! \nReturning to main menu. ");
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
                if (user.Password?.Length < 8)
                {
                    Console.WriteLine("Password must be at least8 characters." +
                                      "\nPlease provide a new password.");
                    Console.WriteLine("\nPlease enter your password: (Must be min 8 characters)");
                    user.Password = Console.ReadLine();
                }

                break;
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            users.Add(user);
            Console.Clear();
            new UserDatabase.UserDatabase(repositoryUser).WriteUser(user);
        }
        
    }
}