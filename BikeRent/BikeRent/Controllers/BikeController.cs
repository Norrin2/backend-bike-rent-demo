using BikeRent.Domain.Entities;
using BikeRent.Publisher.Interfaces;
using BikeRent.Publisher.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BikeRent.Controllers
{
    [ApiController]
    [Route("api/bikes")]
    public class BikeController : ControllerBase
    {
        private readonly ILogger<BikeController> _logger;
        private readonly IBikeService _bikeService;

        public BikeController(ILogger<BikeController> logger, IBikeService bikeService)
        {
            _logger = logger;
            _bikeService = bikeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(Guid id)
        {
            var bike = await _bikeService.FindById(id);

            if (bike == null)
            {
                return NotFound();
            }

            return Ok(bike);
        }


        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] BikeViewModel body)
        {
            var bike = await _bikeService.AddBike(body);

            var notifications = _bikeService.GetNotifications();
            if (notifications.Any())
            {
                var notificationNotFound = notifications.FirstOrDefault(n => n.Key == nameof(Bike));
                if (notificationNotFound != null) 
                {
                    return NotFound(notificationNotFound.Message);
                }

                return BadRequest(string.Join(", ", notifications.Select(n => n.Message)));
            }

            return Ok(bike);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLicensePlate(Guid id, [FromQuery] string licensePlate)
        {
            var bike = await _bikeService.UpdateLicensePlate(id, licensePlate);

            var notifications = _bikeService.GetNotifications();
            if (notifications.Any())
            {
                var notificationNotFound = notifications.FirstOrDefault(n => n.Key == nameof(Bike));
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
