export class SearchOrderDetailsQuery 
{
  productId: number;
  orderDateFrom: Date;
  orderDateTo: Date;
}

export class SearchOrderDetailsResponse {
  orderId: number;
  companyName: number;
  employeeFirstName: number;
  employeeLastName: number;
  orderDate: number;
  shipCountry: number;
}
