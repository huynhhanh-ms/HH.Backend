﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Dto.Account
{
    public class AccountRes
    {
        public string Username { get; set; } = null!;

        public string? Fullname { get; set; }

        public string? Email { get; set; }

        public string PasswordHash { get; set; } = null!;

        public string Role { get; set; } = null!;

    }
}
