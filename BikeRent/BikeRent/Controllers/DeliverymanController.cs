using BikeRent.Domain.Entities;
using BikeRent.Publisher.Interfaces;
using BikeRent.Publisher.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BikeRent.Publisher.Controllers
{
    [ApiController]
    [Route("api/deliveryman")]
    public class DeliverymanController : ControllerBase
    {
        private readonly IDeliverymanService _service;

        public DeliverymanController(IDeliverymanService service)
        {
            _service = service;
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] DeliverymanViewModel body)
        {
            var bike = await _service.Add(body);

            var notifications = _service.GetNotifications();
            if (notifications.Any())
            {
                var notificationNotFound = notifications.FirstOrDefault(n => n.Key == nameof(Deliveryman));
                if (notificationNotFound != null)
                {
                    return NotFound(notificationNotFound.Message);
                }

                return BadRequest(string.Join(", ", notifications.Select(n => n.Message)));
            }

            return Ok(bike);
        }
    }
}
