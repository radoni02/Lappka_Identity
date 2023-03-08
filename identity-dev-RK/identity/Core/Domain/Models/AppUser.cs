using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        
        public string FirstName { get; private set; }
        public string LastName { get;  private set; }
        public string ProfilePicture { get;private set; }

        public Role Role { get; set; }

        public AppUser()
        {
        }

        public AppUser(string firstName, string lastName,string email)
        {
            Role = Role.User;
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName=firstName+lastName+DateTime.UtcNow.ToString(("yyyyMMddHHmmssffff"));
            ProfilePicture = Guid.Empty.ToString();
        }

        public void UpdateUserData(string firstName, string lastName, string profilePicture)
        {
            if (!string.IsNullOrWhiteSpace(firstName))
                FirstName = firstName;
            if (!string.IsNullOrWhiteSpace(lastName))
                LastName = lastName;

            if(!string.IsNullOrWhiteSpace(profilePicture))
            {
                ProfilePicture = profilePicture == Guid.Empty.ToString() ? null : profilePicture;
            }
        }
    }
}
