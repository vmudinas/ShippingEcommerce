using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShippingEcommerce.Data;
using ShippingEcommerce.Exceptions;
using ShippingEcommerce.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShippingEcommerce.Models;
using System;
using System.Linq;
using NodaTime;
using System.Collections.Generic;
using NodaTime.Extensions;

namespace ShippingEcommerce.Services
{
    class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;


        public OrderService(DataContext context, IMapper mapper, ILogger<OrderService> logger,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedList<ProductListItem>> SearchProducts(ProductSearchParams searchParams)
        {
            var orderPredicate = _context.Products.AsNoTracking();
            var projectedOrder = orderPredicate.ProjectTo<ProductListItem>(_mapper.ConfigurationProvider);

            var clock = SystemClock.Instance;
            var zoned = clock.InUtc();

            var pagedList =  await PagedList<ProductListItem>.CreateAsync(projectedOrder, searchParams.PageNumber, searchParams.PageSize);

            foreach (var value in pagedList)
            {
                value.ExpectedShipDate = CalculateExpextedShippingDate(value, searchParams.OrderDate ?? zoned.GetCurrentDate());

            }

            return pagedList;
        }

        public LocalDate CalculateExpextedShippingDate(ProductListItem product, LocalDate orderDate)
        {
            if (product.ShipOnWeekends)
            {
                return orderDate.PlusDays(product.MaxBusinessDaysToShip - 1);
            }
            else if (!product.ShipOnWeekends)
            {
                for (int i = product.MaxBusinessDaysToShip; i >= 0; i--)
                {
                    if (orderDate.DayOfWeek == IsoDayOfWeek.Saturday || orderDate.DayOfWeek == IsoDayOfWeek.Sunday)
                    {
                        i++;
                    }
                    else
                    {
                        orderDate.PlusDays(1);
                    }
                }
            }          
           
           return orderDate;
        }

    }
}