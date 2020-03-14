import { Component, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { ProductsService } from '../../../services/products.service';
import { CategoriesService } from '../../../services/categories.service';
import { SuppliersService } from '../../../services/suppliers.service';
import { NgForm } from '@angular/forms';
import { SearchQueryExtensionEmitterService } from '../../../services/search-query-extension-emitter.service';
import { SearchProductQuery } from '../../../models/IProductSearchQuery';

@Component({
  selector: 'app-product-search',
  templateUrl: './product-search.component.html',
  styleUrls: ['./product-search.component.css']
})
export class ProductSearchComponent implements OnInit {

  categories: any;
  suppliers: any;
  @Output() searchResultEventEmiter = new EventEmitter();
  searchProductQuery = new SearchProductQuery();

  constructor(
    private productsService: ProductsService,
    private categoriesService: CategoriesService,
    private suppliersService: SuppliersService,
    private searchQueryExtensionEmitterService: SearchQueryExtensionEmitterService,
  ) { }

  ngOnInit() {
    this.categories = this.categoriesService.getCategories();
    this.suppliers = this.suppliersService.getSuppliers();
    this.searchQueryExtensionEmitterService.obs.subscribe(x => {
      this.searchProductQuery = { ...this.searchProductQuery, ...x };
      this.search(false);
    });
  }

  search(restorePageIndex: boolean) {

    if (restorePageIndex) {
      this.searchProductQuery.pageIndex = 0;
    }

    console.log(this.searchProductQuery);

    this.productsService.getProducts(this.searchProductQuery).subscribe(x => {
      this.searchResultEventEmiter.emit(x)
    });
  }
}
