using app.Common.Enums;
using System;
using System.Collections.Generic;

namespace app.Operations.ProductOrders.Queries.GetOrderDetails
{
    public class GetOrderDetailsForEditResponse : IGetOrderDetailResponse
	{
		public string CustomerId { get; set; }
		public int? EmployeeId { get; set; }
		public DateTime? OrderDate { get; set; }
		public DateTime? RequiredDate { get; set; }
		public DateTime? ShippedDate { get; set; }
		public string ShipAddress { get; set; }
		public string ShipCity { get; set; }
		public string ShipRegion { get; set; }
		public string ShipPostalCode { get; set; }
		public string ShipCountry { get; set; }
		public int Quantity { get; set; }
		public Dictionary<DropDownListIdentifier, Dictionary<object, string>> DropDownListData { get; set; }
	}
}
