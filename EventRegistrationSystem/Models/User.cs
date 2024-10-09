using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EventRegistrationSystem.Models
{
    public class User : IdentityUser
    {
        public ICollection<Registration> Registrations { get; set; }

    }
}
