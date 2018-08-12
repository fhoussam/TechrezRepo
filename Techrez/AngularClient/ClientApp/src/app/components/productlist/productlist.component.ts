import { Component, OnInit } from '@angular/core';
import { ProductserviceService } from '../../services/productservice.service'

@Component({
  selector: 'app-productlist',
  templateUrl: './productlist.component.html',
  styleUrls: ['./productlist.component.css']
})
export class ProductlistComponent implements OnInit {

  public products : any[] = [];
  constructor(private productserviceService: ProductserviceService) { }
  ngOnInit() {
    this.productserviceService.getProducts().subscribe(data => { this.products = data; });
  }
}
