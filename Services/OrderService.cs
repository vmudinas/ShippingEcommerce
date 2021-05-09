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

namespace ShippingEcommerce.Services
{
    class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;
        private readonly int _maximumOrderDays;
        private readonly int _transitBuffer;

        public OrderService(DataContext context, IMapper mapper, ILogger<OrderService> logger,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _maximumOrderDays = int.Parse(configuration["MaxOrderDays"]);
            _transitBuffer = int.Parse(configuration["TransitBuffer"]);
        }

        public async Task<PagedList<ProductListItem>> SearchOrders(OrderSearchParams searchParams)
        {
            var orderPredicate = _context.Products.AsNoTracking();

            /*
            orderPredicate = orderPredicate.Where(o => o.PartnerId == searchParams.PartnerId);

            if (searchParams.DateFrom is not null && searchParams.DateTo is not null)
            {
                orderPredicate =
                    orderPredicate.Where(o => o.DateFrom >= searchParams.DateFrom || o.DateFrom <= searchParams.DateTo);
            }

            if (searchParams.OrderStatus is not null)
            {
                orderPredicate = orderPredicate.Where(o => o.Status == searchParams.OrderStatus);
            }

            if (searchParams.PaymentStatus is not null)
            {
                orderPredicate = orderPredicate.Where(o => o.Payment.Status == searchParams.PaymentStatus);
            }

            if (!string.IsNullOrWhiteSpace(searchParams.SearchTerm))
            {
                var searchTerm = $"{searchParams.SearchTerm}%";

                orderPredicate = orderPredicate.Where(o =>
                    EF.Functions.Like(o.Id.ToString(), searchTerm) ||
                    EF.Functions.Like(o.Customer.PhoneNumber, searchTerm) ||
                    EF.Functions.Like(o.Customer.ShippingAddress.Address1, searchTerm) ||
                    EF.Functions.Like(o.Customer.ShippingAddress.Address2, searchTerm) ||
                    EF.Functions.Like(o.Customer.ShippingAddress.City, searchTerm) ||
                    EF.Functions.Like(o.Customer.ShippingAddress.State, searchTerm) ||
                    EF.Functions.Like(o.Customer.ShippingAddress.Zipcode, searchTerm) ||
                    EF.Functions.Like(o.Customer.FirstName, searchTerm) ||
                    EF.Functions.Like(o.Customer.LastName, searchTerm) ||
                    EF.Functions.Like(o.Customer.EmailAddress, searchTerm));
            }

            switch (searchParams.SortColumn)
            {
                case null:
                    orderPredicate = searchParams.SortDirection is null ||
                                     searchParams.SortDirection == SortDirection.Asc
                        ? orderPredicate.OrderBy(o => o.OrderPlacedDate).ThenBy(o => o.Id)
                        : orderPredicate.OrderByDescending(o => o.OrderPlacedDate).ThenBy(o => o.Id);
                    break;
                case OrderSortColumns.Id:
                    orderPredicate = searchParams.SortDirection is null ||
                                     searchParams.SortDirection == SortDirection.Asc
                        ? orderPredicate.OrderBy(o => o.Id)
                        : orderPredicate.OrderByDescending(o => o.Id);
                    break;
                case OrderSortColumns.FullName:
                    orderPredicate = searchParams.SortDirection is null ||
                                     searchParams.SortDirection == SortDirection.Asc
                        ? orderPredicate.OrderBy(o => o.Customer.FirstName + " " + o.Customer.LastName).ThenBy(o => o.Id)
                        : orderPredicate.OrderByDescending(o => o.Customer.FirstName + " " + o.Customer.LastName).ThenBy(o => o.Id);
                    break;
                case OrderSortColumns.OrderDate:
                    orderPredicate = searchParams.SortDirection is null ||
                                     searchParams.SortDirection == SortDirection.Asc
                        ? orderPredicate.OrderBy(o => o.OrderPlacedDate).ThenBy(o => o.Id)
                        : orderPredicate.OrderByDescending(o => o.OrderPlacedDate).ThenBy(o => o.Id);
                    break;
                case OrderSortColumns.DeliveryDate:
                    orderPredicate = searchParams.SortDirection is null ||
                                     searchParams.SortDirection == SortDirection.Asc
                        ? orderPredicate.OrderBy(o => o.DateFrom).ThenBy(o => o.Id)
                        : orderPredicate.OrderByDescending(o => o.DateFrom).ThenBy(o => o.Id);
                    break;
                case OrderSortColumns.ReturnDate:
                    orderPredicate = searchParams.SortDirection is null ||
                                     searchParams.SortDirection == SortDirection.Asc
                        ? orderPredicate.OrderBy(o => o.DateTo).ThenBy(o => o.Id)
                        : orderPredicate.OrderByDescending(o => o.DateTo).ThenBy(o => o.Id);
                    break;
                case OrderSortColumns.ExpectedShipDate:
                    //TODO: Update once we have this value
                    orderPredicate = searchParams.SortDirection is null ||
                                     searchParams.SortDirection == SortDirection.Asc
                        ? orderPredicate.OrderBy(o => o.DateFrom).ThenBy(o => o.Id)
                        : orderPredicate.OrderByDescending(o => o.DateFrom).ThenBy(o => o.Id);
                    break;
                case OrderSortColumns.ExpectedReturnDate:
                    //TODO: Update once we have this value
                    orderPredicate = searchParams.SortDirection is null ||
                                     searchParams.SortDirection == SortDirection.Asc
                        ? orderPredicate.OrderBy(o => o.DateTo).ThenBy(o => o.Id)
                        : orderPredicate.OrderByDescending(o => o.DateTo).ThenBy(o => o.Id);
                    break;
                case OrderSortColumns.OrderStatus:
                    orderPredicate = searchParams.SortDirection is null ||
                                     searchParams.SortDirection == SortDirection.Asc
                        ? orderPredicate.OrderBy(o => o.Status).ThenBy(o => o.DateFrom).ThenBy(o => o.Id)
                        : orderPredicate.OrderByDescending(o => o.Status).ThenBy(o => o.DateFrom).ThenBy(o => o.Id);
                    break;
                case OrderSortColumns.Total:
                    orderPredicate = searchParams.SortDirection is null ||
                                     searchParams.SortDirection == SortDirection.Asc
                        ? orderPredicate.OrderBy(o => o.Total).ThenBy(o => o.Id)
                        : orderPredicate.OrderByDescending(o => o.Total).ThenBy(o => o.Id);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            */
            var projectedOrder = orderPredicate.ProjectTo<ProductListItem>(_mapper.ConfigurationProvider);
            return await PagedList<ProductListItem>.CreateAsync(projectedOrder, searchParams.PageNumber, searchParams.PageSize);
        }

    }
}