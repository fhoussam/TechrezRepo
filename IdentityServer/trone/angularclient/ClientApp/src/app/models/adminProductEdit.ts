import { adminProductListItem } from './adminProductListItem';

export class adminProductEdit {
    code: string;
    description: string;
    quantity: number;
    price: number;
    categoryId: number;
    photoUrl: string;

    constructor() { }

    toAdminProductListItem(): adminProductListItem {
        let listItem = new adminProductListItem();
        listItem.code = this.code;
        listItem.categoryId = this.categoryId;
        listItem.description = this.description;
        listItem.price = this.price;
        listItem.quantity = this.quantity;
        listItem.photoUrl = this.photoUrl;
        return listItem;
    }
}
