import { Component, OnInit } from '@angular/core';
import { ProductserviceService } from '../../services/productservice.service'

@Component({
  selector: 'app-productlist',
  templateUrl: './productlist.component.html',
  styleUrls: ['./productlist.component.css']
})
export class ProductlistComponent implements OnInit {

  public products: any[] = [];
  public aa: any[] = [];
  constructor(private productserviceService: ProductserviceService) { }
  ngOnInit() {
    this.refreshList();
  }

  refreshList() {
    this.productserviceService.getProducts().subscribe(data => { this.products = data; });
  }

  deleteProdut(id: number) {
    console.log('deleting product : ' + id);
    
    this.productserviceService.deleteProduct(id).subscribe(data => {
      console.log('deleted');
      this.refreshList();
    });;
    
  }
}
