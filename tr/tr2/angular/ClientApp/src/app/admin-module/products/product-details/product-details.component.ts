import { Component, OnInit } from '@angular/core';
import { IProductDetails } from '../../../models/IProductDetails';
import { ProductsService } from '../../../services/products.service';
import { ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {

  product: IProductDetails;

  constructor(
    private productsService: ProductsService,
    private activatedRoute: ActivatedRoute,
  ) { }

  ngOnInit() {
    this.activatedRoute.paramMap.subscribe((params: ParamMap) => {
      this.productsService.getProduct(+params.get('id')).subscribe((x: IProductDetails) => {
        this.product = x;
      });
    });
  }
}
