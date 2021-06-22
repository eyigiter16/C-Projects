using System;
using System.Collections.Generic;
using System.Linq;
using Reviews.Models;
using Reviews.Service.AdminService.Interface;

namespace Reviews.Service.AdminService
{
    public class AdminService : IAdminService
    {
        public int LoginAdmin(IEnumerable<Admin> admins)
        {
            Console.WriteLine("\nTo return back                                   : 1 " +
                              "\nPlease enter your User Name:");
            var u = Console.ReadLine();
            if (string.Equals(u, "1"))
            {
                Console.Clear();
                Console.WriteLine("\nReturning to main menu. ");
                return 0;
            }
            Console.WriteLine("\nTo return back                                   : 1 " +
                              "\nPlease enter your Password:");
            var p = Console.ReadLine();
            Console.Clear();
            if (string.Equals(p, "1"))
            {
                Console.WriteLine("\nReturning to main menu. ");
                return 0;
            }
            foreach (var admin in admins.Where(admin => string.Equals(admin.UserName, u)))
            {
                if (string.Equals(admin.Password, p))
                {
                    return 1;
                }
                Console.WriteLine("\nWrong Password! ");
            }
            Console.WriteLine("\nInvalid admin login attempt! \nReturning to main menu. ");
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