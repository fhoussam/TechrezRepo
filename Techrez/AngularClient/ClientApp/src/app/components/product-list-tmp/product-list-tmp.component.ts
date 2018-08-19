import { Component, OnInit } from '@angular/core';
import { ProductserviceService } from '../../services/productservice.service'
import { Product } from '../../Models/Product';
import { List } from 'linqts';

@Component({
  selector: 'app-product-list-tmp',
  templateUrl: './product-list-tmp.component.html',
  styleUrls: ['./product-list-tmp.component.css'],
})
export class ProductListTmpComponent implements OnInit {

  public products: Product[] = [];
  public sortField : string = 'id';
  public isDesc: boolean = false;
  public isDataLoaded = false;

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

  refreshList() {
    this.productserviceService.getProducts().subscribe(data => {
      this.products = data;
      this.isDataLoaded = true;
    });
  }
}
