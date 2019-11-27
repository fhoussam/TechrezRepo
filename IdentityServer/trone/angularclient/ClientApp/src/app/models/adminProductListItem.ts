export class adminProductListItem {
    code: string;
    description: string;
    quantity: number;
    price: number;
    categoryId: number;

    constructor() {
        this.code = '';
        this.description = '';
        this.quantity = 0;
        this.price = 0;
        this.categoryId = 0;
    }
}
