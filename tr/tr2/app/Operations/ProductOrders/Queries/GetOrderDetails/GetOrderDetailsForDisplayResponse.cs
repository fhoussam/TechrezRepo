using app.Operations.ProductOrders.Queries.GetOrderDetails;
using System;

namespace app.Operations.ProductOrders.Queries.GetOrderDetails
{
    public class GetOrderDetailsForDisplayResponse : IGetOrderDetailResponse
	{
		public int OrderId { get; set; }
		public int Quantity { get; set; }
		public string CompanyName { get; set; }
		public string EmployeeFirstName { get; set; }
		public string EmployeeLastName { get; set; }
		public DateTime? OrderDate { get; set; }
		public DateTime? RequiredDate { get; set; }
		public DateTime? ShippedDate { get; set; }
		public string ShipAddress { get; set; }
		public string ShipCity { get; set; }
		public string ShipRegion { get; set; }
		public string ShipPostalCode { get; set; }
		public string ShipCountry { get; set; }
	}
}
