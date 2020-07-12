using Microsoft.EntityFrameworkCore;
using PackageDetails.CORE.Models.Identity;
using PackageDetails.CORE.Repositories.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageDetails.DAL.Repositories.Identity
{
    public class AuthRepository : Repository<User>, IAuthRepository
    {

        public AuthRepository(PackageDetailsContext context)
         : base(context)
        { }

        private PackageDetailsContext _context
        {
            get { return Context as PackageDetailsContext; }
        }




        public async Task<User> Login(string email, string password)
        {
            var user = await _context.User.Where(x => !x.IsDeleted).FirstOrDefaultAsync(x => x.Email.Equals(email));

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);


            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;


            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> IsUserExistByEmail(string email)
        {
            return await _context.User.AnyAsync(x => x.Email.Equals(email));
        }


        /*Privat methods*/
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

     
    }
}
