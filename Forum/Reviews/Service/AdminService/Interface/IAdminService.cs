using System.Collections.Generic;
using Reviews.Models;

namespace Reviews.Service.AdminService.Interface
{
    public interface IAdminService
    {
        int LoginAdmin(IEnumerable<Admin> admins);
        void CreateAdmin(List<Admin> admins);
    }
}