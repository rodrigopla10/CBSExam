using CBSExam.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBSExam.ViewModel
{
    public class MessageViewModel
    {

        [Required]
        [RegularExpression("^\\+?\\d*$", ErrorMessage = "Enter a valid phone number [0-9]")]
        [Display(Name = "Phone number:")]
        public string to { get; set; }

        [Required]
        [Display(Name = "Message:")]
        public string message { get; set; }

        public string twilioResponse { get; set; }

        public IEnumerable<Message> messages{ get; set; }
    }
}
