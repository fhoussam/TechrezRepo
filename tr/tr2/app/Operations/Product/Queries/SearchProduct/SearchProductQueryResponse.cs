using System;
using System.Collections.Generic;
using System.Text;

namespace app.Operations.Product.Queries.SearchProduct
{
    public class SearchProductQueryResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
