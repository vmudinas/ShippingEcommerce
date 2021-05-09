using System.Collections.Generic;
using ShippingEcommerce.Data.Entities;

namespace ShippingEcommerce.Data.SeedData
{
    public static class ProductSeed
    {
        public static readonly IEnumerable<Product> Data = new List<Product>
        {
            new()
            {
                ProductId =  1,
                ProductName = "fugiat exercitation adipisicing",
                InventoryQuantity = 43,
                ShipOnWeekends = true,
                MaxBusinessDaysToShip = 13
            },
            new()
            {
                ProductId =  2,
                ProductName = "mollit cupidatat Lorem",
                InventoryQuantity = 70,
                ShipOnWeekends = true,
                MaxBusinessDaysToShip = 18
            },
            new()
            {
                ProductId =  3,
                ProductName = "non quis sint",
                InventoryQuantity = 33,
                ShipOnWeekends = false,
                MaxBusinessDaysToShip = 15
            },
            new()
            {
                ProductId =  4,
                ProductName = "voluptate cupidatat non",
                InventoryQuantity = 52,
                ShipOnWeekends = false,
                MaxBusinessDaysToShip = 18
            },
            new()
            {
                ProductId =  5,
                ProductName = "anim amet occaecat",
                InventoryQuantity = 39,
                ShipOnWeekends = true,
                MaxBusinessDaysToShip = 19
            },
            new()
            {
                ProductId =  6,
                ProductName = "cillum deserunt elit",
                InventoryQuantity = 47,
                ShipOnWeekends = false,
                MaxBusinessDaysToShip = 20
            },
            new()
            {
                ProductId =  7,
                ProductName = "adipisicing reprehenderit et",
                InventoryQuantity = 71,
                ShipOnWeekends = false,
                MaxBusinessDaysToShip = 15
            },
            new()
            {
                ProductId =  8,
                ProductName = "ex mollit laboris",
                InventoryQuantity = 80,
                ShipOnWeekends = false,
                MaxBusinessDaysToShip = 15
            }
        };
    }
}