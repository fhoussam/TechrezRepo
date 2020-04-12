import { Pager } from "./Pager";

export class SearchProductQuery extends Pager {
  productName: string;
  supplierId: number;
  categoryId: number;
  MinUnitsInStock: number;
  MaxUnitsInStock: number;
  discontinued: boolean;
}
