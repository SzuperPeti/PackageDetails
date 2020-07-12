using PackageDetails.CORE.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PackageDetails.CORE.Services.Identity
{
    public interface IAuthService
    {
        Task<bool> IsUserExistByEmail(string email);
        Task<User> Register(User user, string password);
        Task<User> Login(string email, string password);
        Task<User> GetUserById(int id);

    }
}
