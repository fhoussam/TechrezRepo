export enum DropDownListIdentifier {
  Employees = "Employees",
  Suppliers = "Suppliers",
  Categories = "Categories",
  Customers = "Customers",
  Countries = "Countries",
}

export interface DdlKeyValue {
  key: string | number;
  value: string;
}

export interface DropDownListData {
  identifier: DropDownListIdentifier;
  data: DdlKeyValue[];
}

export type DropDownListDictionary = {
  [key in DropDownListIdentifier]: DdlKeyValue[]
}
