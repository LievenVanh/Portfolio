using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace BrolIndexer.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "Gebruikersnaam")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Huidig wachtwoord")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} moet ten minste {2} karakters lang zijn.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nieuw wachtwoord")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestig wachtwoord")]
        [Compare("NewPassword", ErrorMessage = "Het nieuw wachtwoord komt niet overeen met bevestig wachtwoord.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Gebruikersnaam")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [Display(Name = "Onthouden?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Gebruikersnaam")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} moet ten minste {2} karakters lang zijn.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestig wachtwoord")]
        [Compare("Password", ErrorMessage = "Het nieuw wachtwoord komt niet overeen met bevestig wachtwoord.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
