import { SearchSetting } from "../components/commom/SearchSetting";

export class productFilter extends SearchSetting{
    description: string;
    minStock: number;
    maxStock: number;
    minPrice: number;
    maxPrice: number;
    categoryID: number;
}