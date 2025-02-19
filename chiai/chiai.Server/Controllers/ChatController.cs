using chiai.Server.Sevices.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace chiai.Server.Controllers
{
    [Route("chat")]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
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

    }
}
