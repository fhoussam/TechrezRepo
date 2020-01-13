export class ProductSearchParams {
    description: string;
    categoryId: number;
    constructor(description, categoryId) {
        this.description = description;
        this.categoryId = categoryId;
    }
}
