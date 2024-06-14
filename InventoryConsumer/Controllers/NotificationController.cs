using NotificationConsumer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NotificationConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger _logger;

        public NotificationController(ILogger logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public IActionResult ProcessNotificationUpdate([FromBody] NotificationUpdateRequest request)
        {
            return Ok("Notification update processed successfully.");
        }
    }
}
