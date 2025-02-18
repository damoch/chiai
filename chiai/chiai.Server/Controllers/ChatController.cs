using Microsoft.AspNetCore.Mvc;

namespace chiai.Server.Controllers
{
    [Route("chat")]
    public class ChatController : Controller
    {
        public ChatController()
        {
            
        }
        [HttpGet(Name = "new/{userId}")]
        public IActionResult StartNewChat(string userId)
        {
            return Ok("TestID");
        }
    }
}
