import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges, SimpleChange } from '@angular/core';
import { Form } from '@angular/forms'
import { product } from 'src/app/models/product';
import { category } from 'src/app/models/category';
import { APP_SETTINGS } from 'src/app/models/APP_SETTINGS';
import { ProductService } from 'src/app/services/product.service';
 
@Component({
  selector: 'productedit',
  templateUrl: './productedit.component.html',
  styleUrls: ['./productedit.component.css']
})
export class ProducteditComponent implements OnInit, OnChanges {

  ngOnChanges(changes: SimpleChanges): void {
    this.productService.getProduct(changes.productid.currentValue)
      .subscribe(data => {  this.product = data; });
  }

  product: product;
  @Input() productid: number;
  @Output() productUpdateCanceled = new EventEmitter<product>();
  @Output() productUpdated = new EventEmitter<product>();
  
  public categories: category[] = [];
  
  constructor(private productService:ProductService) { }

  updateProduct() {
    this.productService.updateProduct(this.product).subscribe(data => { 
      this.productUpdated.emit(data); 
      console.log('product updated'); 
    });
  }

  ngOnInit() {
    this.categories = APP_SETTINGS.categories;
  }

  cancelUpdate()
  {
    this.productService.getProduct(this.product.id).subscribe(data => 
      this.productUpdateCanceled.emit(data));
      console.log('product update canceled'); 
  }
}
