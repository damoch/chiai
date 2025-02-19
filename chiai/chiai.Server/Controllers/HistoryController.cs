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

        [HttpGet]
        public IActionResult GetHistory([FromQuery] int userId)
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
    }
}
