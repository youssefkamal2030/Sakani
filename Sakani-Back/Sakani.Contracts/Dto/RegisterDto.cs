﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakani.Contracts.Dto
{
    public class RegisterDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string password { get; set; }
       
    }
}
