import { SearchQueryExtension } from "./ISearchQueryExtension";

export class SearchProductQuery extends SearchQueryExtension {
  productName: string;
  supplierId: number;
  categoryId: number;
  MinUnitsInStock: number;
  MaxUnitsInStock: number;
  discontinued: boolean;
}
