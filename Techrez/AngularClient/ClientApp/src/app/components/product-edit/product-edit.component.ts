import { Component, OnInit, Input } from '@angular/core';
import { Form } from '@angular/forms'
import { Product } from '../../Models/Product'
import { ProductserviceService } from '../../services/productservice.service'
import { CaregoryService } from '../../services/categories.service';
import { Category } from '../../Models/Category';
 
@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css']
})
export class ProductEditComponent implements OnInit {

  @Input() product: Product;
  
  public categories: Category[] = [];

  log(message) {
    console.log(message);
  }

  constructor(private productService: ProductserviceService, private caregoryService: CaregoryService) { }

  updateProduct() {
    this.productService.updateProduct(this.product).subscribe(data => { console.log('product updated'); });
  }

  getCategories(){
    return this.caregoryService.getCategories().subscribe(data => { this.categories = data; });
  }

  ngOnInit() {
    this.getCategories();
  }
}
