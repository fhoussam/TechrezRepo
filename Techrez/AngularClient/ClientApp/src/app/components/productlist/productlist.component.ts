import { Component, OnInit } from '@angular/core';
import { ProductserviceService } from '../../services/productservice.service'
import { Product } from '../../Models/Product';
import { List } from 'linqts';

@Component({
  selector: 'app-productlist',
  templateUrl: './productlist.component.html',
  styleUrls: ['./productlist.component.css'],
})
export class ProductlistComponent implements OnInit {

  public products: Product[] = [];
  public selectedProduct: Product;
  public sortField : string = 'id';
  public isDesc: boolean = false;

  constructor(private productserviceService: ProductserviceService) { }
  ngOnInit() {
    this.refreshList();
  }

  sort() {
    if (this.isDesc) {
      this.products = new List<Product>(this.products).OrderByDescending(x => x[this.sortField]).ToArray();
    }
    else
      this.products = new List<Product>(this.products).OrderBy(x => x[this.sortField]).ToArray();
  }

  toggleSortOrder() {
    this.isDesc = !this.isDesc;
    this.sort();
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
