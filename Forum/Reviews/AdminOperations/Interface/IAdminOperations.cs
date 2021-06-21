using System.Collections.Generic;
using Reviews.Models;

namespace Reviews.AdminOperations.Interface
{
    public interface IAdminOperations
    {
        int LoginAdmin(IEnumerable<Admin> admins);
        void CreateAdmin(List<Admin> admins);
    }
}