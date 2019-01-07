export class product {
    id: number;
    description: string;
    stock: number;
    price: number;
    categoryID: number;
}

export class productSearchResult
{
    pageData : product[];
    count : number;
}

export interface DialogData {
  animal: string;
  name: string;
}
