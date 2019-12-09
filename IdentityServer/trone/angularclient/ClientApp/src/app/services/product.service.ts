import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { adminProductListItem } from '../models/adminProductListItem';
import { adminProductEdit } from '../models/adminProductEdit';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

    
    constructor(private http: HttpClient) { }

    getProducts() {
        return this.http.get<adminProductListItem[]>('http://localhost:5001/api/product');
    }

    save(productFormData : adminProductEdit, productImage : File) {
        const fd = new FormData();
        if (productImage) fd.append("productImage", productImage, productImage.name);
        fd.append("productData", JSON.stringify(productFormData));
        return this.http.post("http://localhost:5001/api/product/save", fd);
    }
}
