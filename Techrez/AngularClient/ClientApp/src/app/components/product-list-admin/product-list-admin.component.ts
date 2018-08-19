import { Component, OnInit } from '@angular/core';
import { ProductserviceService } from '../../services/productservice.service'
import { Product } from '../../Models/Product';
import { List } from 'linqts';

@Component({
  selector: 'app-product-list-admin',
  templateUrl: './product-list-admin.component.html',
  styleUrls: ['./product-list-admin.component.css'],
})
export class ProductListAdminComponent implements OnInit {

  public products: Product[] = [];
  public selectedProduct: Product;

  constructor(private productserviceService: ProductserviceService) { }
  ngOnInit() {
    this.refreshList();
  }

  getTrBackgroundColor(product: Product):string {
    if (product === this.selectedProduct)
      return '#b8d6d6';
    else if (product.stock < 10)
      return '#e6b5b5';
    else
      return 'white';
  }

  refreshList() {
    this.productserviceService.getProducts().subscribe(data => {
      this.products = data;
      if (this.products.length > 0) {
        this.selectedProduct = this.products[0];
      } 
    });
  }

  selectProduct(product : Product):void {
    this.selectedProduct = product;
  }

  deleteProdut(id: number) {
    this.productserviceService.deleteProduct(id).subscribe(data => {
      this.refreshList();
    });;
  }
}
