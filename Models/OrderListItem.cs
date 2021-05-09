using NodaTime;

namespace ShippingEcommerce.Models
{
    public class ProductListItem
    {
        public int Id { get; set; }

        public LocalDate DateTo { get; set; }

        public LocalDate DateFrom { get; set; }

        public LocalDate ExpectedReturnDate { get; set; }

        public LocalDate ExpectedShipDate { get; set; }

        public LocalDate OrderPlacedDate { get; set; }

        public decimal Total { get; set; }

    }
}
