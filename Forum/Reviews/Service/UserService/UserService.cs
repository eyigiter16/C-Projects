using System;
using System.Collections.Generic;
using System.Linq;
using Reviews.Exception;
using Reviews.Models;
using Reviews.Mongo;
using Reviews.Repository;
using Reviews.Service.UserService.Interface;
using Reviews.Validation;

namespace Reviews.Service.UserService
{
    public class UserService : IUserService
    {
        private UserValidator _validator;
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
            while(true)
            {
                _validator = new UserValidator();
                Console.WriteLine("\nPlease enter your first name: ");
                user.FirstName = Console.ReadLine();
                Console.WriteLine("\nPlease enter your last name: ");
                user.LastName = Console.ReadLine();
                Console.WriteLine("\nPlease enter your email: ");
                user.Email = Console.ReadLine();
                while (true)
                {
                    if (!users.Any(user1 => string.Equals(user1.Email, user.Email)))
                    {
                        break;
                    }
                    Console.WriteLine("This email is already on the use by another user." +
                                      "\nPlease provide a new email.");
                    Console.WriteLine("\nPlease enter your email: ");
                    user.Email = Console.ReadLine();
                }
                
                Console.WriteLine("\nPlease enter your password: (8 to 24 characters)");
                user.Password = Console.ReadLine();
                Console.Clear();
                var validate = _validator.Validate(user);
                if (validate.IsValid)
                {
                    break;
                }
                Console.WriteLine(validate);
                try
                {
                    throw new InvalidRegister();
                }
                catch (InvalidRegister e)
                {
                    Console.WriteLine(e.Code+e.Message);
                }
                var choice = Console.ReadLine();
                Console.Clear();
                if (!string.Equals(choice, "1")) return;
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            users.Add(user);
            Console.Clear();
            new UserRepository(repositoryUser).WriteUser(user);
        }
        //mongo end point gets kullan
        // asp.net core bak çalış
    }
}