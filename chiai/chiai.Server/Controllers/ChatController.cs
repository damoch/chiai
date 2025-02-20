using chiai.Server.Data.Dto;
using chiai.Server.Messages.Commands;
using chiai.Server.Sevices.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace chiai.Server.Controllers
{
    [Route("chat")]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        private readonly IAiChatService _aiChatService;
        private readonly IMediator _mediator;

        public ChatController(IChatService chatService, IMediator mediator, IAiChatService aiChatService)
        {
            _chatService = chatService;
            _aiChatService = aiChatService;
            _mediator = mediator;
        }

        [HttpGet("new/{userId}")]
        public async Task<IActionResult> StartNewChat(int userId)
        {
            try
            {
                var chatDto = await _chatService.StartNewChat(userId);
                return Ok(chatDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("send/{chatId}")]
        public async Task<IActionResult> SendMessage(int chatId, [FromBody] ChatMessageDto message)
        {
            try
            {
                await _mediator.Send(new SaveChatMessageCommand(chatId, message));
                var response =  _aiChatService.GenerateResponseStreamAsync(message.Content, chatId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("rate/{chatId}/{messageId}/{rating}")]
        public async Task<IActionResult> RateMessage(int chatId, int messageId, int rating)
        {
            try
            {
                await _chatService.RateMessageAsync(chatId, messageId, rating);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
