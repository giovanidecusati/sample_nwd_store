using System;

namespace Nwd.BackOffice.SalesContext.Commands.Outputs
{
    public class GetProductListResult
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
    }
}
