using PackageDetails.CORE.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PackageDetails.CORE.Repositories.Identity
{
    public interface IAuthRepository : IRepository<User>
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string email, string password);
        Task<bool> IsUserExistByEmail(string email);

    }
}
