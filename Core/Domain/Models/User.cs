using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Core.Domain.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string BackupEmail { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime? LastPasswordChangeTime { get; set; }

        public List<Claim> SelectedClaims { get; set; } = new List<Claim>();

        public string FullName => FirstName + " " + LastName;

        public List<Claim> GetClaims()
        {
            return SelectedClaims;
        }
    }

}
