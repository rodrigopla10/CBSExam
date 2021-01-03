using System.Collections.Generic;

namespace CBSExam.Models
{
    public interface IMessageRepository
    {

        IEnumerable<Message> ListPrevMessages();
        int InsertMessage(Message message);

        void InsertSentMessage(SentMessage sentMessage);


    }
}
