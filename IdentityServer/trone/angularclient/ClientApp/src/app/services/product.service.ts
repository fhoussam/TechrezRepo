import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { adminProductListItem } from '../models/adminProductListItem';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

    
    constructor(private http: HttpClient) { }

    getProducts() {
        return this.http.get<adminProductListItem[]>('http://localhost:5001/api/product');
    }
}
