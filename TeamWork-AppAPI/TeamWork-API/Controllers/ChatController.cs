using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TeamWork_API.Utils;
using Microsoft.AspNetCore.Http;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.DataAccess.Domain.Account.Domain;
using TeamWork.DataAccess.Domain.Models.Domain;
using TeamWork_API.ErrorHandler;
using TeamWork.DataAccess.Domain.Chat.Domain;
using System.Collections.Generic;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : BaseController
    {
        private readonly IChatService _chatService;
        public ChatController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IChatService chatService) :base(configuration, httpContextAccessor)
        {
            _chatService = chatService;
        }

        [HttpGet]
        [Route("GetMessages")]
        public async Task<IActionResult> GetMessages(string key)
        {
            var chatKey = await _chatService.GetChatByGroupKeyAsync(key);

            if (chatKey == null)
            {
                return StatusCode(Codes.Number_200, null);
            }

            var messages = await _chatService.GetMessagesByChatKeyAsync(chatKey.ChatID.ToString());

            var messagesView = new List<MessageView>();

            foreach(var message in messages)
            {
                messagesView.Add(new MessageView
                {
                    MessageKey = message.ID.ToString(),
                    Content = message.Content,
                    DateSent = message.DateSent.ToString(),
                    UserName = message.User?.FirstName + " " + message.User?.LastName
                });
            }
            return StatusCode(Codes.Number_200, messagesView);
        }

        [HttpPost]
        [Route("SaveMessage")]
        public async Task<IActionResult> SaveMessage(MessageRecevied messageRecevied)
        {
            if (messageRecevied == null)
            {
                return StatusCode(Codes.Number_204, NoContent204Error.NoContent);
            }

            var message = new Message
            {
                Content = messageRecevied.Content,
                DateSent = DateTime.Now,
                ID = Guid.NewGuid(),
                UserId = ExtractEmailFromJWT()
            };

            if(!(await _chatService.SaveMessageByGroupKeyAsync(messageRecevied.GroupKey,message)))
            {
                return StatusCode(Codes.Number_400, BadRequest400Error.SomethingWentWrong);
            }
                
            return Ok();
        }

        [HttpPut]
        [Route("UpdateMessage")]
        public async Task<IActionResult> UpdateMessage(MessageUpdate messageUpdate)
        {
            if (messageUpdate == null)
            {
                return StatusCode(Codes.Number_204, NoContent204Error.NoContent);
            }

            var messageOld = await _chatService.GetMessageByKeyAsync(messageUpdate.MessageKey);
            messageOld.Content = messageUpdate.Content;

            if (!(await _chatService.UpdateMessageAsync(messageOld)))
            {
                return StatusCode(Codes.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }
        [HttpDelete]
        [Route("DeleteMessage")]
        public async Task<IActionResult> DeleteMessage(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return StatusCode(Codes.Number_204, NoContent204Error.NoContent);
            }

            if (!(await _chatService.DeleteMessageAsync(key)))
            {
                return StatusCode(Codes.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }
    }
}