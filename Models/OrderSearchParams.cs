using Microsoft.AspNetCore.Mvc;
using NodaTime;

namespace ShippingEcommerce.Models
{
    public class OrderSearchParams : PaginationParams
    {     

        [FromQuery(Name = "f")]
        public LocalDate? DateFrom { get; set; }

        [FromQuery(Name = "t")]
        public LocalDate? DateTo { get; set; }

        [FromQuery(Name = "s")]
        public string SearchTerm { get; set; }
        
        [FromQuery(Name = "sort_col")]
        public OrderSortColumns? SortColumn { get; set; }

        [FromQuery(Name = "sort_dir")]
        public SortDirection? SortDirection { get; set; }
    }

    public class PaginationParams 
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }

    public enum SortDirection
    {
        Asc,
        Desc
    }

    public enum OrderSortColumns
    {
        Id,
        FullName,
        OrderDate,
        DeliveryDate,
        ReturnDate,
        ExpectedShipDate,
        ExpectedReturnDate,
        OrderStatus,
        Total
    }
}