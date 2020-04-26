import { Pager } from "./Pager";
import { DropDownListDictionary } from "./config-models";

export class SearchOrderDetailsQuery extends Pager
{
  productId: number;
  orderDateFrom: Date;
  orderDateTo: Date;
}

export class GetOrderDetailsForDisplayResponse {
  orderId: number;
  quantity: number;
  companyName: string;
  employeeFirstName: string;
  employeeLastName: string;
  orderDate: Date;
  requiredDate: Date;
  shippedDate: Date;
  shipAddress: string;
  shipCity: string;
  shipRegion: string;
  shipPostalCode: string;
  shipCountry: string;
}

export class GetOrderDetailsForEditResponse {
  orderId: number;
  quantity: number;
  customerId: string;
  employeeId: string;
  orderDate: Date;
  requiredDate: Date;
  shippedDate: Date;
  shipAddress: string;
  shipCity: string;
  shipRegion: string;
  shipPostalCode: string;
  shipCountry: string;
  dropDownListData: DropDownListDictionary[];
}

export class SearchOrderDetailsResponse {
  orderId: number;
  CompanyName: string;
  EmployeeFirstName: string;
  EmployeeLastName: string;
  OrderDate: Date;
  ShipCountry: string;
}

export type OrderDetails = GetOrderDetailsForDisplayResponse | GetOrderDetailsForEditResponse;

export class EditOrderDetailCommand
{
  orderId: number;
  productId: number;
  customerId: string;
  employeeId: number;
  quantity: number;
  orderDate: Date;
  requiredDate: Date;
  shippedDate: Date;
  shipAddress: string;
  shipPostalCode: string;
}
