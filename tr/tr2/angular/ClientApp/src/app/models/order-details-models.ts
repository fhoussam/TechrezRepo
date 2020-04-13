import { Pager } from "./Pager";

export class SearchOrderDetailsQuery extends Pager
{
  productId: number;
  orderDateFrom: Date;
  orderDateTo: Date;
}

export class GetOrderDetailForDisplayResponse {
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
  Quantity: number;
  CustomerId: string;
  EmployeeId: string;
  OrderDate: Date;
  RequiredDate: Date;
  ShippedDate: Date;
  ShipAddress: string;
  ShipCity: string;
  ShipRegion: string;
  ShipPostalCode: string;
  ShipCountry: string;
  DropDownListData: any;
}

export class SearchOrderDetailsResponse {
  OrderId: number;
  CompanyName: string;
  EmployeeFirstName: string;
  EmployeeLastName: string;
  OrderDate: Date;
  ShipCountry: string;
}

export type OrderDetails = GetOrderDetailForDisplayResponse | GetOrderDetailsForEditResponse;

export class EditOrderDetailCommand
{
  OrderId: number;
  ProductId: number;
  CustomerId: string;
  EmployeeId: number;
  Quantity: number;
  OrderDate: Date;
  RequiredDate: Date;
  ShippedDate: Date;
  ShipAddress: string;
  ShipPostalCode: string;
}

export enum DropDownListIdentifier {
  Employees = 0,
  Suppliers = 1,
  Categories = 2,
  Customers = 3,
}
