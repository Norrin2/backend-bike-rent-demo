using BikeRent.Domain.Entities;
using BikeRent.Publisher.Interfaces;
using BikeRent.Publisher.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BikeRent.Publisher.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService orderService)
        {
            _service = orderService;
        }


        [HttpPost()]
        [ProducesResponseType(typeof(Deliveryman), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] PlaceOrderViewModel body)
        {
            var order = await _service.PlaceOrder(body.Value);

            return Ok(order);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Order>), StatusCodes.Status200OK)]
        public async Task<IActionResult> FindAll()
        {
            var entities = await _service.FindAll();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindById(Guid id)
        {
            var bike = await _service.FindById(id);

            if (bike == null)
            {
                return NotFound();
            }

            return Ok(bike);
        }
    }
}
