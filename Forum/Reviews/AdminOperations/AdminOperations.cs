using System;
using System.Collections.Generic;
using System.Linq;
using Reviews.AdminOperations.Interface;
using Reviews.Models;

namespace Reviews.AdminOperations
{
    public class AdminOperations : IAdminOperations
    {
        public int LoginAdmin(IEnumerable<Admin> admins)
        {
            Console.WriteLine("\nPlease enter your User Name:");
            var u = Console.ReadLine();
            Console.WriteLine("\nPlease enter your Password:");
            var p = Console.ReadLine();
            Console.Clear();
            
            foreach (var admin in admins.Where(admin => string.Equals(admin.UserName, u)))
            {
                if (string.Equals(admin.Password, p))
                {
                    return 1;
                }
                Console.WriteLine("\nWrong Password! ");
            }
            Console.WriteLine("\nInvalid admin login attempt!\n Returning to main menu.");
            return 0;
        }

        public void CreateAdmin(List<Admin> admins)
        {
            for (var i = 0; i < 2; i++)
            {
                var id = Guid.NewGuid();
                var admin = new Admin {Id = id, UserName = "admin" + Convert.ToString(i + 1), Password = "1234"};
                admins.Add(admin);
            }
        }
    }
}