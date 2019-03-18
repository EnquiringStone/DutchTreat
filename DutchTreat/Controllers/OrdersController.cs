using DutchTreat.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository repository;
        private readonly ILogger<OrdersController> logger;

        public OrdersController(
            IDutchRepository repository,
            ILogger<OrdersController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(repository.GetAllOrders());
            }
            catch (Exception exception)
            {
                logger.LogError($"Failed to get orders: {exception}");
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var order = repository.GetOrderById(id);

                if (order != null)
                {
                    return Ok(order);
                }

                return NotFound();
            }
            catch (Exception exception)
            {
                logger.LogError($"Failed to get orders: {exception}");
                return BadRequest();
            }
        }
    }
}
