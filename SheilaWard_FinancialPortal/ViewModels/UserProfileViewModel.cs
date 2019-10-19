using System;
using System.Collections.Generic;
using SheilaWard_FinancialPortal.Models;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SheilaWard_FinancialPortal.ViewModels
{
    public class UserProfileViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(25, ErrorMessage = "Cannot be longer than 40 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "Last Name cannot be greater than 40 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [MaxLength(50, ErrorMessage = "Full Name cannot be greater than 50 characters")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        [Display(Name = "Avatar path")]
        public string AvatarUrl { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public HttpPostedFileBase Avatar { get; set; }

        public int? HouseholdId { get; set; }
    }
}