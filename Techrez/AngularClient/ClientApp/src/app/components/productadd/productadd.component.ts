import { Component, OnInit, Input } from '@angular/core';
import { Form } from '@angular/forms'
import { Product } from '../../Models/Product'
import { ProductserviceService } from '../../services/productservice.service'
 
@Component({
  selector: 'app-productadd',
  templateUrl: './productadd.component.html',
  styleUrls: ['./productadd.component.css']
})
export class ProductaddComponent implements OnInit {

  @Input() product: Product;

  log(message){
    console.log(message);
  }

  constructor(private productService: ProductserviceService ) { }

  updateProduct() {
    this.productService.updateProduct(this.product).subscribe(data => { console.log('product updated'); });
  }

  ngOnInit() {
    
  }
}
