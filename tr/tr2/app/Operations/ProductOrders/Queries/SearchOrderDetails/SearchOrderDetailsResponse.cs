using System;
using System.Collections.Generic;
using System.Text;

namespace app.Operations.ProductOrders.Queries.SearchOrderDetails
{
    public class SearchOrderDetailsResponse
    {
        public int OrderId { get; set; }
        public string CompanyName { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public DateTime? OrderDate { get; set; }
        public string ShipCountry { get; set; }
    }
}
