﻿using System.ComponentModel.DataAnnotations;

namespace Library.Management.System.APIs.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]

        public string Password { get; set; }
    }
}
