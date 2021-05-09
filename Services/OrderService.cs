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
            return await PagedList<ProductListItem>.CreateAsync(projectedOrder, searchParams.PageNumber, searchParams.PageSize);
        }

    }
}