﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrustedB.Models.ViewModels
{
    public class TopicVM
    {
        public Topics topic { get; set; }
        //public StateHistory stateHistory { get; set; }
        //public IdentityUser user { get; set; }
       
    }
}
