using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<StoreUser> userManager;

        public DutchSeeder(
            DutchContext context,
            IHostingEnvironment hostingEnvironment,
            UserManager<StoreUser> userManager)
        {
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }

        public async Task SeedAsync()
        {
            context.Database.EnsureCreated();

            var user = await userManager.FindByEmailAsync("johan-langes@hotmail.com");
            if (user == null)
            {
                user = new StoreUser
                {
                    FirstName = "Johan",
                    LastName = "Langes",
                    Email = "johan-langes@hotmail.com",
                    UserName = "johan-langes@hotmail.com"
                };

                var result = await userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create a new user.");
                }
            }

            if (!context.Products.Any())
            {
                // Need to create sample data
                var filePath = Path.Combine(hostingEnvironment.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);

                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                context.Products.AddRange(products);

                var order = context.Orders.Where(o => o.Id == Guid.Parse("4313ebfd-6948-4759-ad11-5e40337a7f94")).FirstOrDefault();
                if (order != null)
                {
                    order.User = user;
                    order.Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    };
                }

                context.SaveChanges();
            }
        }
    }
}
