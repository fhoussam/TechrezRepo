import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { adminProductListItem } from '../models/adminProductListItem';
import { adminProductEdit } from '../models/adminProductEdit';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
    
    constructor(
        private http: HttpClient,
        private cookie: CookieService
    ) { }

    getProducts() {
        return this.http.get<adminProductListItem[]>('https://localhost:44301/api/product');
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
