using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBSExam.Models
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _appDbContext;

        public MessageRepository(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }

        public void CreateMessage(Message message)
        {
            try
            {
                message.createdDate = DateTime.Now;

                _appDbContext.Message.Add(message);
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
