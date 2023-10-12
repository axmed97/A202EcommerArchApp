﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete.EntityFramework
{
    public class AppUser : IdentityUser, IEntity 
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
