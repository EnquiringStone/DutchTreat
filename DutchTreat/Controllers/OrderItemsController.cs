using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/orders/{orderid}/items")]
    public class OrderItemsController : Controller
    {
        private readonly IDutchRepository repository;
        private readonly ILogger<OrderItemsController> logger;
        private readonly IMapper mapper;

        public OrderItemsController(
            IDutchRepository repository,
            ILogger<OrderItemsController> logger,
            IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(string orderId)
        {
            try
            {
                var order = repository.GetOrderById(orderId);
                if (order != null) return Ok(mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));

                return NotFound();
            }
            catch (Exception exception)
            {
                logger.LogError($"Failed to get order items{exception}");
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(string orderId, string id)
        {
            try
            {
                var order = repository.GetOrderById(orderId);
                Debug.WriteLine("test");
                var orderItem = order.Items.SingleOrDefault(o => o.Id == Guid.Parse(id));
                if (orderItem != null) return Ok(mapper.Map<OrderItem, OrderItemViewModel>(orderItem));

                return NotFound();
            }
            catch (Exception exception)
            {
                logger.LogError($"Failed to get order item: {exception}");
                return BadRequest();
            }
        }
    }
}
