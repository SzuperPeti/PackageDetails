using PackageDetails.CORE;
using PackageDetails.CORE.Models.Identity;
using PackageDetails.CORE.Services.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PackageDetails.BLL.Services.Identity
{
    public class AuthService : IAuthService
    {

        private readonly IUnitOfWork _unitOfWork;
        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _unitOfWork.Identity.GetByIdAsync(id);
        }

        public async Task<bool> IsUserExistByEmail(string email)
        {
            return await _unitOfWork.Identity.IsUserExistByEmail(email);
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _unitOfWork.Identity.Login(email, password);

            if (user == null)
                return null;

            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;


            await _unitOfWork.Identity.AddAsync(user);
            await _unitOfWork.CommitAsync();

            return user;
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
