using IdentityJWT.Models.DTO;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;
using WeaponAuthorization.Data;

namespace IdentityJWT.Helpers
{
    public static class AuthenticationHelper
    {
        public static IdentityUser CreateNewUser(HeroDTO heroDTO)
        {
            IdentityUser newUser = new IdentityUser(heroDTO.UserName);
            newUser.Email = heroDTO.Email;
            //using (var hmac = new HMACSHA512())
            //{
            //    var hasedPasswordBytes =  hmac.ComputeHash(Encoding.UTF8.GetBytes(heroDTO.Password));
            //    newUser.PasswordHash = Encoding.UTF8.GetString(hasedPasswordBytes);
            //}
            return newUser;

        }

        public static bool VerifyUser(string username, string password, HeroIdentityContext heroIdentityContext)
        {
            if (username == null || password == null) {  return false; }
            string hashedPassword = String.Empty;
            using (var hmac = new HMACSHA512())
            {
                var hasedPasswordBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                hashedPassword = Encoding.UTF8.GetString(hasedPasswordBytes);
            }
            return heroIdentityContext.Users.Any(u => u.UserName == username && u.PasswordHash.Equals(hashedPassword));
        }

    }
}
