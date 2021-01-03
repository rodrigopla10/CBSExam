using CBSExam.Models;
using CBSExam.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBSExam.Controllers
{
    public class PhoneController : Controller
    {

        private readonly IMessageRepository _messageRepository;

        public PhoneController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Message> messages = null;

            try
            {
                messages = _messageRepository.ListPrevMessages();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message.ToString());
            }

            return View(new MessageViewModel { messages = messages });
        }

        [HttpPost]
        public IActionResult SubmitMessage(MessageViewModel messageViewModel)
        {
            Message message = new Message();

            try
            {
                if (ModelState.IsValid)
                {
                    message.to = messageViewModel.to;
                    message.message = messageViewModel.message;
                    _messageRepository.CreateMessage(message);
                }
                else
                {
                    messageViewModel.messages = new List<Message>();
                    return View(messageViewModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message.ToString());
            }

            return RedirectToAction("Index");

        }

    }
}
