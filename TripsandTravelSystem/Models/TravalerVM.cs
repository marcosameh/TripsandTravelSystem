﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripsandTravelSystem.Models
{
    public class TravalerVM
    {
        public int TravalerId { get; set; }

        [Required(ErrorMessage = ("Youy Must Enter Name"))]
        [MinLength(3, ErrorMessage = "Short name")]
        [MaxLength(20, ErrorMessage = "invaild name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, ErrorMessage = "Must be between 6 and 30 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(30, ErrorMessage = "Must be between 6 and 30 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "You Must Enter Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public string Photo { get; set; }
    }
}