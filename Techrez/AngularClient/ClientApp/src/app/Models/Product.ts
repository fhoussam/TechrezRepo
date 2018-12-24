export class product {
    id: number;
    description: number;
    stock: number;
    price: number;
    categoryID: number;
}

export class productSearchResult
{
    pageData : product[];
    count : number;
}