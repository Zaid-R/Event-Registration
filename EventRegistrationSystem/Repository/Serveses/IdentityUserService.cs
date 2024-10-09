using EventRegistrationSystem.Data;
using EventRegistrationSystem.Models;
using EventRegistrationSystem.Models.DTO;
using EventRegistrationSystem.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EventRegistrationSystem.Repository.Services
{
    public class IdentityUserService : IAccount
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public IdentityUserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Register new user
        public async Task<UserDto> Register(RegisterUserDTO userData, ModelStateDictionary modelState)
        {
            var user = new User()
            {
                UserName = userData.UserName,
                Email = userData.Email
            };

            var result = await _userManager.CreateAsync(user, userData.Password);

            if (result.Succeeded)
            {
                return new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
            }

            // Add registration errors to ModelState
            foreach (var error in result.Errors)
            {
                var errorCode = error.Code.Contains("Password") ? nameof(userData.Password) :
                                error.Code.Contains("Email") ? nameof(userData.Email) :
                                error.Code.Contains("UserName") ? nameof(userData.UserName) : "";

                modelState.AddModelError(errorCode, error.Description);
            }

            return null;
        }

        // Login user
        public async Task<UserDto> LoginUser(string Username, string Password)
        {
            var user = await _userManager.FindByNameAsync(Username);
            if (user == null)
            {
                return null; // User not found
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, Password, false, false);
            if (result.Succeeded)
            {
                return new UserDto()
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
            }

            return null; // Invalid credentials
        }

    }
}
