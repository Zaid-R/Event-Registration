using EventRegistrationSystem.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EventRegistrationSystem.Repository.Interfaces
{
    public interface IAccount
    {
       public Task<UserDto> Register(RegisterUserDTO registerUserDTO, ModelStateDictionary modelState);

      public  Task<UserDto> LoginUser(string Username, string Password);

      
    }
}
