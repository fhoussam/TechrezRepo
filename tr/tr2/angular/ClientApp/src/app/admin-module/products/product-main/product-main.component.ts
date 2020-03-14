import { Component, OnInit } from '@angular/core';
import { IProductSearchResponse } from '../../../models/IProductSearchResponse';

@Component({
  selector: 'app-product-main',
  templateUrl: './product-main.component.html',
  styleUrls: ['./product-main.component.css']
})
export class ProductMainComponent implements OnInit {

  searchResult: IProductSearchResponse;

  constructor() { }

  ngOnInit() {
  }

  passDataToParent(searchResult) {
    this.searchResult = searchResult;
  }
}
