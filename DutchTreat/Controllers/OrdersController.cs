using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
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
        private readonly IMapper mapper;

        public OrdersController(
            IDutchRepository repository,
            ILogger<OrdersController> logger,
            IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(repository.GetAllOrders()));
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
                    return Ok(mapper.Map<Order, OrderViewModel>(order));
                }

                return NotFound();
            }
            catch (Exception exception)
            {
                logger.LogError($"Failed to get orders: {exception}");
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var order = mapper.Map<OrderViewModel, Order>(model);

                    if (order.OrderDate == DateTime.MinValue)
                    {
                        order.OrderDate = DateTime.Now;
                    }

                    repository.AddEntity(order);
                    if (repository.SaveAll())
                    {
                        var viewModel = mapper.Map<Order, OrderViewModel>(order);
                        return Created($"/api/orders/{viewModel.OrderId}", viewModel);
                    }
                }
                

                return BadRequest(ModelState);
            }
            catch (Exception exception)
            {
                logger.LogError($"Failed to save a new order: {exception}");
                return BadRequest();
            }
        }
    }
}
