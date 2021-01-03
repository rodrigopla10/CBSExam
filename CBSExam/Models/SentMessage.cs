using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBSExam.Models
{
    public class SentMessage
    {
        [Key]
        public int sentMessageID { get; set; }


        public DateTime dateSent { get; set; }

        public string confirmationCode { get; set; }

        [ForeignKey("Message")]
        public int messageID { get; set; }
        public Message message { get; set; }

    }
}
