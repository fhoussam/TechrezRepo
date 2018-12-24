import { 
  Component, 
  OnInit, 
  Input, Output, EventEmitter,
  OnChanges, SimpleChanges, SimpleChange
} from '@angular/core';
import { product } from 'src/app/models/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'productdetails',
  templateUrl: './productdetails.component.html',
  styleUrls: ['./productdetails.component.css']
})
export class ProductdetailsComponent implements OnInit, OnChanges {

  ngOnChanges(changes: SimpleChanges): void {
    const productid: SimpleChange = changes.productid;
    // console.log('prev value: ', productid.previousValue);
    // console.log('got productid: ', productid.currentValue);
    this._productid = productid.currentValue;
    this.productService.getProduct(this._productid).subscribe(data => this.product = data);
  }

  product:product;
  private _productid:number;
  @Input() productid:number;
  @Output() public childEvent = new EventEmitter<boolean>();

  constructor(private productService:ProductService) { }

  ngOnInit() {
  }

  editProduct()
  {
    this.childEvent.emit(true);
  }
}
