﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustedB.Models
{
    public class Container
    {
        [Required]
        public string Name { get; set; }
    }
}
