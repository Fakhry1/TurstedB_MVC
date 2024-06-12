using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrustedB.Models.ViewModels
{
    public class RequestDetailsVM
    {
        public Topics? topic { get; set; }
        public List<Attachments>? AttachmentsList { get; set; }
        public List<Topics>? TopicList { get; set; }
         //List<List<Attachments> myList = new List<List<int>>() { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

        //List<List<Attachments>> Test { get; set; }

}
}
