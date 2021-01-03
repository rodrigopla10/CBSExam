using CBSExam.Helper;
using CBSExam.Models;
using CBSExam.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CBSExam.Controllers
{
    public class PhoneController : Controller
    {

        private readonly IMessageRepository _messageRepository;
        public IConfiguration _configuration;

        public PhoneController(IMessageRepository messageRepository, IConfiguration configuration)
        {
            _messageRepository = messageRepository;
            _configuration = configuration;
        }

        public IActionResult Index(string twilioResponse, string sortMessageID)
        {
            IEnumerable<Message> messages = null;

            try
            {
                ViewData["messagenum"] = string.IsNullOrEmpty(sortMessageID) ? "messageID" : "";
                messages = _messageRepository.ListPrevMessages();

                switch (sortMessageID)
                {
                    case "messageID":
                        messages = messages.OrderBy(x => x.messageID);
                        break;
                    default:
                        messages = messages.OrderByDescending(x => x.messageID);
                        break;
                }

                if (!String.IsNullOrEmpty(twilioResponse))
                {
                    twilioResponse = $"Twilio response: {twilioResponse}";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, Utilities.ErrorCodes.LoadingPageError.ToString());
                ModelState.AddModelError("Error: ", Utilities.ErrorCodes.LoadingPageError.ToString());
                return View(new MessageViewModel { messages = messages, twilioResponse = twilioResponse });
            }

            return View(new MessageViewModel { messages = messages, twilioResponse = twilioResponse });
        }

        [HttpPost]
        public IActionResult SubmitMessage(MessageViewModel messageViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Message message = new Message();
                    SentMessage sentMessage = new SentMessage();
                    TwilioKeys keys = _configuration.GetSection("TwilioKeys").Get<TwilioKeys>();

                    message.to = messageViewModel.to;
                    message.message = messageViewModel.message;

                    TwilioClient.Init(keys.SID, keys.token);

                    var msj = MessageResource.Create(
                        body: messageViewModel.message,
                        from: new Twilio.Types.PhoneNumber(keys.twilioPhone),
                        to: new Twilio.Types.PhoneNumber(messageViewModel.to)
                    );

                    message.createdDate = (DateTime)msj.DateCreated;

                    string statusResponse = msj.Status.ToString();

                    sentMessage.confirmationCode = statusResponse;
                    sentMessage.dateSent = (DateTime)(msj.DateSent == null ? msj.DateCreated : msj.DateSent);

                    sentMessage.messageID = _messageRepository.InsertMessage(message);
                    _messageRepository.InsertSentMessage(sentMessage);

                    Log.Information($"Message submitted succesfully and got Twilio response:{statusResponse}");

                    return RedirectToAction("Index", new { twilioResponse = statusResponse });
                }
                else
                {
                    messageViewModel.messages = new List<Message>();
                    return View(messageViewModel);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, Utilities.ErrorCodes.SubmitMessageError.ToString());
                messageViewModel.messages = new List<Message>();
                ModelState.AddModelError("Error: ", Utilities.ErrorCodes.SubmitMessageError.ToString());
            }
            return View("Index", messageViewModel);
        }

    }
}
