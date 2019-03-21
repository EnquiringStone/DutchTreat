using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext context;
        private readonly ILogger<DutchRepository> logger;

        public DutchRepository(
            DutchContext context,
            ILogger<DutchRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public void AddEntity(object model)
        {
            context.Add(model);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            try
            {
                if (includeItems)
                {
                    return context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .ToList();
                }
                return context.Orders
                    .ToList();
            }
            catch (Exception exception)
            {
                logger.LogError($"Failed to get all orders: {exception}");
                return null;
            }
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            try
            {
                if (includeItems)
                {
                    return context.Orders
                    .Where(o => o.User.UserName == username)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .ToList();
                }
                return context.Orders
                    .Where(o => o.User.UserName == username)
                    .ToList();
            }
            catch (Exception exception)
            {
                logger.LogError($"Failed to get all orders: {exception}");
                return null;
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                logger.LogInformation("Getting products");

                return context.Products.OrderBy(p => p.Title).ToList();
            }
            catch (Exception exception)
            {
                logger.LogError($"Failed to get all products: {exception}");
                return null;
            }
            
        }

        public Order GetOrderById(string username, string id)
        {
            try
            {
                return context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .Where(o => o.Id == Guid.Parse(id) && o.User.UserName == username)
                    .SingleOrDefault();
            }
            catch (Exception exception)
            {
                logger.LogError($"Failed to get all orders: {exception}");
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return context.Products.Where(p => p.Category == category).ToList();
        }

        public bool SaveAll()
        {
            return context.SaveChanges() > 0;
        }
    }
}
