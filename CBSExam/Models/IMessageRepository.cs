using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBSExam.Models
{
   public interface IMessageRepository
    {

        IEnumerable<Message> ListPrevMessages();
        int InsertMessage(Message message);

        void InsertSentMessage(SentMessage sentMessage);


    }
}
