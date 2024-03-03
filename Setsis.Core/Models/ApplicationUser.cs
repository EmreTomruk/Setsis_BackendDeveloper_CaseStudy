﻿using Microsoft.AspNetCore.Identity;

namespace Setsis.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
