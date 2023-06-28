using Angular_Test_App.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Angular_Test_App.Services
{
    public class UserService
    {
        private readonly WaxHandballAppDbContext _context;
        private PasswordHasher<User> PasswordHasher { get; }

        public UserService(WaxHandballAppDbContext context)
        {
            _context = context;
            PasswordHasher = new PasswordHasher<User>();
        }

        public async Task AddUser(UserRegistrationTemp user)
        {
            var isPasswordCorrect = PasswordConfirmation(user.Password, user.ConfirmPassword);
            if (isPasswordCorrect)
            {
                User newUser = new User() 
                {
                    FirstName=user.FirstName,
                    LastName=user.LastName,
                    Username = user.Username,
                    Email = user.Email,
            };
                newUser.PasswordHash = PasswordHasher.HashPassword(newUser, user.Password);
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
            } 
        }

        public async Task<User?> GetUser(LoginCredentials loginCredentials)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == loginCredentials.Username);
        }

        public async Task<IEnumerable<Claim>> CreateClaims(LoginCredentials loginCredentials)
        {
            var user = await GetUser(loginCredentials);

            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, user.Username),
                new (ClaimTypes.Email, user.Email),
                new ("UserId", user.Id.ToString()),
            };

            return claims;
        }

        public async Task<bool> LoginIsValid(LoginCredentials loginData)
        {
            var user = await GetUser(loginData);
            if (user == null)
            {
                return false;
            }

            var result = PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, loginData.Password);
            return result == PasswordVerificationResult.Success;

        }

        private bool PasswordConfirmation(string password, string confirmPassword)
        {
            if (password == null || confirmPassword == null) { return false; }
            else if (password != confirmPassword) { return false; }
            else { return true; }
        }
    }
}
