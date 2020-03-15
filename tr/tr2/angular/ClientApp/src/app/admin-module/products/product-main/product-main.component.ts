import { Component, OnInit, ViewChild } from '@angular/core';
import { IProductSearchResponse, IProductListItem } from '../../../models/IProductSearchResponse';
import { GridField } from '../../../models/GridField';
import { SuppliersService } from '../../../services/suppliers.service';
import { CategoriesService } from '../../../services/categories.service';
import { ProductsService } from '../../../services/products.service';
import { SearchProductQuery } from '../../../models/IProductSearchQuery';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-product-main',
  templateUrl: './product-main.component.html',
  styleUrls: ['./product-main.component.css']
})
export class ProductMainComponent implements OnInit {

  searchResult: IProductSearchResponse;
  selectedItem: IProductListItem;
  gridFields: GridField[];
  categories: any;
  suppliers: any;
  searchProductQuery = new SearchProductQuery();
  @ViewChild('f', { static: false }) searchForm: NgForm

  constructor(
    private productsService: ProductsService,
    private categoriesService: CategoriesService,
    private suppliersService: SuppliersService,
  ) {

    this.gridFields = [
      new GridField("ProductId", "Product Id", true),
      new GridField("ProductName", "Product Name", false),
      new GridField("SupplierId", "Supplier", false),
      new GridField("CategoryId", "Category", false),
      new GridField("QuantityPerUnit", "Quantity Per Unit", false),
      new GridField("UnitPrice", "Unit Price", false),
    ];

    this.searchProductQuery.isDesc = false;
    this.searchProductQuery.pageIndex = 0;
    this.searchProductQuery.sortField = this.gridFields[1].fieldName;
  }

  ngOnInit() {
    this.categories = this.categoriesService.getCategories();
    this.suppliers = this.suppliersService.getSuppliers();
  }

  emitPageIndex(pageIndex: number) {
    this.searchProductQuery.pageIndex = pageIndex;
    this.search(false);
  }

  reset() {
    this.searchProductQuery = new SearchProductQuery();
  }

  emitSortField(event, isHidden) {

    if (isHidden)
      return;

    var target = event.target || event.srcElement || event.currentTarget;
    var idAttr = target.attributes.id.value;

    if (this.searchProductQuery.sortField == idAttr)
      this.searchProductQuery.isDesc = !this.searchProductQuery.isDesc;
    else
      this.searchProductQuery.sortField = idAttr;

    this.search(false);
  }

  search(isFromUi: boolean) {
    if (isFromUi)
      this.searchProductQuery.pageIndex = 0;
    this.productsService.getProducts(this.searchProductQuery).subscribe(x => this.searchResult = x);
  }

  selectItem(item) {
    this.selectedItem = item;
    //navigate to selected tab or just to the id
    //on success of navigate, emit selected item to update changes back
  }
}
