using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBSExam.Models
{
    public class Message
    {

        [Key]
        public int messageID { get; set; }

        public DateTime createdDate { get; set; }

        public string to { get; set; }

        public string message { get; set; }
    }
}
