using NotificationProducer.Models;
using NotificationProducer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace NotificationProducer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ProducerService _producerService;

        public NotificationController(ProducerService producerService)
        {
            _producerService = producerService;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNotification([FromBody] NotificationUpdateRequest request)
        {
            var message = JsonSerializer.Serialize(request);

            await _producerService.ProduceAsync("NotificationUpdates", message);

            return Ok("Notification Updated Successfully...");
        }
    }
}
