using chiai.Server.Messages.Commands;
using chiai.Server.Sevices.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace chiai.Server.Controllers
{
    [Route("history")]
    public class HistoryController : Controller
    {
        private readonly IMediator _mediator;

        public HistoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetHistory(int userId)
        {
            try
            {
                var history = await _mediator.Send(new GetUserChatHistoryQuery(userId));
                return Ok(history);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("chats/{chatId}/messages")]
        public async Task<IActionResult> GetChatMessages(int chatId)
        {
            try
            {
                var chatDto = await _mediator.Send(new GetChatMessagesCommand(chatId));
                return Ok(chatDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
