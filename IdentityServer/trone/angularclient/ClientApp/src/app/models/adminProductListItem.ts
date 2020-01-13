export class adminProductListItem {
    code: string;
    description: string;
    photoUrl: string;
    quantity: number;
    price: number;
    categoryId: number;

    constructor() {
        this.code = null;
        this.description = '';
        this.photoUrl = '';
        this.quantity = 0;
        this.price = 0;
        this.categoryId = 0;
    }
}
