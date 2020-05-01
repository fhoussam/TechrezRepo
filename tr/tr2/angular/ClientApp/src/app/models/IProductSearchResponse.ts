export interface SearchProductQueryResponse {
  selected: boolean;
  productId: number;
  productName: string;
  supplierId: number;
  categoryId: number;
  quantityPerUnit: string;
  unitPrice: number;
}
