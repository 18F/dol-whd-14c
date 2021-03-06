﻿using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.ViewModels
{
    public class VerifyResetPasswordViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Nounce { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
