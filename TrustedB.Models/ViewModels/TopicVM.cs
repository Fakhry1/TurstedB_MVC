using Microsoft.AspNetCore.Identity;
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
        public Topics? topic { get; set; }
        public Attachments? attachments { get; set; }
        public CommentHistory? comments { get; set; }

        public List<StateHistory>? StateHistoryList  { get; set; }
        public List<Attachments>? AttachmentsList { get; set; }
        public List<CommentHistory>? CommentList { get; set; }


    }
}
