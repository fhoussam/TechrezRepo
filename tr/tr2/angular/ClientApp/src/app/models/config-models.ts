export enum DropDownListIdentifier {
  Employees = "Employees",
  Suppliers = "Suppliers",
  Categories = "Categories",
  Customers = "Customers",
  Countries = "Countries",
}

export interface KeyValue {
  key: string | number;
  value: string;
}

export interface DropDownListData {
  identifier: DropDownListIdentifier;
  data: KeyValue[];
}
