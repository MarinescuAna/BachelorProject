using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using System.Collections.Generic;
using TeamWork.DataAccess.Domain.ChatDTO;
using TeamWork.DataAccess.Domain.Models;
using TeamWork.Common.ConstantNumbers;
using TeamWork.Common.ConstantStrings;
using TeamWork.Common.ConstantStrings.ErrorHandler;

namespace TeamWork_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : BaseController
    {
        private readonly IChatService _chatService;
        public ChatController(
            IHttpContextAccessor httpContextAccessor,
            IChatService chatService) :
            base(httpContextAccessor)
        {
            _chatService = chatService;
        }

        [HttpGet]
        [Route("GetMessages")]
        public async Task<IActionResult> GetMessages(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return StatusCode(Number.Number_200, null);
            }

            var messages = await _chatService.GetMessagesByGroupKeyAsync(key);

            var messagesView = new List<MessageView>();

            foreach(var message in messages)
            {
                messagesView.Add(new MessageView
                {
                    MessageKey = message.ID.ToString(),
                    Content = message.Content,
                    DateSent = message.DateSent.ToString(),
                    UserName = message.User?.FirstName + Constants.BlankSpace + message.User?.LastName
                });
            }
            return StatusCode(Number.Number_200, messagesView);
        }

        [HttpPost]
        [Route("SaveMessage")]
        public async Task<IActionResult> SaveMessage(MessageRecevied messageRecevied)
        {
            if (messageRecevied == null)
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            var message = new Message
            {
                GroupID=Guid.Parse(messageRecevied.GroupKey),
                Content = messageRecevied.Content,
                DateSent = DateTime.Now,
                ID = Guid.NewGuid(),
                UserId = ExtractEmailFromJWT()
            };

            if(!await _chatService.SaveMessageByGroupKeyAsync(message))
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }
                
            return Ok();
        }

        [HttpPut]
        [Route("UpdateMessage")]
        public async Task<IActionResult> UpdateMessage(MessageUpdate messageUpdate)
        {
            if (messageUpdate == null)
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            var messageOld = await _chatService.GetMessageByKeyAsync(messageUpdate.MessageKey);
            messageOld.Content = messageUpdate.Content;

            if (!await _chatService.UpdateMessageAsync(messageOld))
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("DeleteMessage")]
        public async Task<IActionResult> DeleteMessage(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return StatusCode(Number.Number_204, NoContent204Error.NoContent);
            }

            if (!await _chatService.DeleteMessageAsync(key))
            {
                return StatusCode(Number.Number_400, BadRequest400Error.SomethingWentWrong);
            }

            return Ok();
        }
    }
}