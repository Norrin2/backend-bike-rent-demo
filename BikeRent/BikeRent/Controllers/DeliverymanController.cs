using BikeRent.Domain.Entities;
using BikeRent.Publisher.Interfaces;
using BikeRent.Publisher.Service;
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
        [ProducesResponseType(typeof(Deliveryman), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] DeliverymanViewModel body)
        {
            var deliveryman = await _service.Add(body);

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

            return Ok(deliveryman);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Deliveryman), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindById(Guid id)
        {
            var entity = await _service.FindById(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Deliveryman>), StatusCodes.Status200OK)]
        public async Task<IActionResult> FindAll()
        {
            var entities = await _service.FindAll();
            return Ok(entities);
        }

        [HttpPost("rent")]
        [ProducesResponseType(typeof(Deliveryman), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Rent([FromBody] BikeRentViewModel body)
        {
            var deliveryman = await _service.RentBike(body);

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

            return Ok(deliveryman);
        }


        [HttpPost("finish-rent")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FinishRent([FromBody] FinishRentViewModel body)
        {
            var cost = await _service.FinishRentAndGetCost(body);

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

            return Ok(cost);
        }
    }
}
