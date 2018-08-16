import { Component, OnInit } from '@angular/core';
import { ProductserviceService } from '../../services/productservice.service'
import { IProduct } from '../../Models/Product';

@Component({
  selector: 'app-productlist',
  templateUrl: './productlist.component.html',
  styleUrls: ['./productlist.component.css'],
})
export class ProductlistComponent implements OnInit {

  public products: any[] = [];
  public selectedProduct: IProduct;
  public sortField : string = 'description';
  public isDesc: boolean = false;

  constructor(private productserviceService: ProductserviceService) { }
  ngOnInit() {
    this.refreshList();
    this.selectedProduct = this.products[0];
  }

  getTrBackgroundColor(product: IProduct):string {
    if (product === this.selectedProduct)
      return 'azure';
    else if (product.stock < 10)
      return 'burlywood';
    else
      return 'white';
  }

  refreshList() {
    this.productserviceService.getProducts().subscribe(data => {
      this.products = data;
      if(this.products.length > 0) this.selectedProduct = this.products[0];
    });
  }

  selectProduct(product : IProduct):void {
    this.selectedProduct = product;
  }

  deleteProdut(id: number) {
    this.productserviceService.deleteProduct(id).subscribe(data => {
      this.refreshList();
    });;
  }
}
