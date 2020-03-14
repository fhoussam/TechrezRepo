export interface IProductSearchResponse {
  totalPages: number;
  source: IProductListItem[];
}

export interface IProductListItem {
  productId: number;
  productName: string;
  supplierId: number;
  categoryId: number;
  quantityPerUnit: number;
  unitPrice: number;
}
