using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBSExam.Models
{
   public interface IMessageRepository
    {

        IEnumerable<Message> ListPrevMessages();
        void CreateMessage(Message message);


    }
}
