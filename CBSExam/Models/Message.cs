using System;
using System.ComponentModel.DataAnnotations;

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
