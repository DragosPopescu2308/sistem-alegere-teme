﻿using Microsoft.AspNetCore.Identity;

namespace MyApp.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
    }
}
