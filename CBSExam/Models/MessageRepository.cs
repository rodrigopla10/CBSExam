using System;
using System.Collections.Generic;

namespace CBSExam.Models
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _appDbContext;

        public MessageRepository(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }

        public int InsertMessage(Message message)
        {
            try
            {
                _appDbContext.Message.Add(message);
                _appDbContext.SaveChanges();

                return message.messageID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public void InsertSentMessage(SentMessage sentMessage)
        {
            try
            {
                _appDbContext.SentMessage.Add(sentMessage);
                _appDbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public IEnumerable<Message> ListPrevMessages()
        {
            try
            {
                return _appDbContext.Message;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
