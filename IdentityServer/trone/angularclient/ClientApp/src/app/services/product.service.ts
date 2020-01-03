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
        private cookie: CookieService
    ) { }

    getProducts(searchParams: ProductSearchParams) {
        var params = new HttpParams();
        params.append("CategoryId", searchParams.CategoryId.toString());
        params.append("Description", searchParams.Description);

        var headers = new HttpHeaders({
            'Content-Type': 'application/json'
        });

        const httpOptions : any = {
            headers: { 'Content-Type': 'application/json' },
            params: { 'CategoryId': searchParams.CategoryId.toString(), 'Description': searchParams.Description }
        };

        return this.http.get<adminProductListItem[]>('https://localhost:44301/api/product', httpOptions);
    }

    save(productFormData: adminProductEdit, productImage: File) {

        let cookieValue = this.cookie.get('XSRF-REQUEST-TOKEN');

        const httpOptions = {
            headers: new HttpHeaders({
                'X-XSRF-TOKEN': cookieValue
            })
        };

        const fd = new FormData();
        if (productImage) fd.append("productImage", productImage, productImage.name);
        fd.append("productData", JSON.stringify(productFormData));
        return this.http.post("https://localhost:44301/api/product/save", fd, httpOptions);
    }
}
