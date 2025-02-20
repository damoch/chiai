using chiai.Server.Sevices.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace chiai.Server.Controllers
{
    [Route("history")]
    public class HistoryController : Controller
    {
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet("{userId}")]
        public IActionResult GetHistory(int userId)
        {
            try
            {
                var history = _historyService.GetHistory(userId);
                return Ok(history);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("chats/{chatId}/messages")]
        public IActionResult GetChatMessages( int chatId)
        {
            try
            {
                var chatDto = _historyService.GetChatMessages(chatId);
                return Ok(chatDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
