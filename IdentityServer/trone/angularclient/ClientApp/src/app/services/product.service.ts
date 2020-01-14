import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http'
import { adminProductListItem } from '../models/adminProductListItem';
import { adminProductEdit } from '../models/adminProductEdit';
import { CookieService } from 'ngx-cookie-service';
import { ProductSearchParams } from '../models/productSearchParam';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
    
    constructor(
        private http: HttpClient,
    ) { }

    getProducts(searchParams: ProductSearchParams) {

        //because angular doesnt support sending null value instead of emty 
        var httpParams: any = new ProductSearchParams();
        if (!!searchParams.categoryId) httpParams.categoryId = searchParams.categoryId;
        if (!!searchParams.description) httpParams.description = searchParams.description;

        const httpOptions : any = {
            headers: { 'Content-Type': 'application/json' },
            params: httpParams
        };

        return this.http.get<adminProductListItem[]>('https://localhost:44301/api/product', httpOptions);
    }

    save(productFormData: adminProductEdit, productImage: File) {

        const fd = new FormData();
        if (productImage) fd.append("productImage", productImage, productImage.name);
        fd.append("productData", JSON.stringify(productFormData));
        return this.http.post("https://localhost:44301/api/product/save", fd);
    }

    getProduct(code: string) {
        return this.http.get('https://localhost:44301/api/product/' + code);
    }
}
