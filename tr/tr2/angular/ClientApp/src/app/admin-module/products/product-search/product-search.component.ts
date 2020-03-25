import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { IProductSearchResponse } from '../../../models/IProductSearchResponse';
import { SuppliersService } from '../../../services/suppliers.service';
import { CategoriesService } from '../../../services/categories.service';
import { ProductsService } from '../../../services/products.service';
import { SearchProductQuery } from '../../../models/SearchProductQuery';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-product-search',
  templateUrl: './product-search.component.html',
  styleUrls: ['./product-search.component.css']
})
export class ProductSearchComponent implements OnInit, OnDestroy {

  searchResult: IProductSearchResponse;
  routeSubscription: Subscription;
  categories: any;
  suppliers: any;
  searchProductQuery : SearchProductQuery;
  @ViewChild('f', { static: false }) searchForm: NgForm;
  searchPanelCollapsed: boolean;
  autoCollapse: boolean;
  selectedItemId: number;
  searchErrorMessage: string;

  collapse() {
    this.searchPanelCollapsed = !this.searchPanelCollapsed;
  }

  closeErrorDialog() {
    this.searchErrorMessage = null;
  }

  constructor(
    private productsService: ProductsService,
    private categoriesService: CategoriesService,
    private suppliersService: SuppliersService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
  ) {
    this.searchPanelCollapsed = false;
    this.searchProductQuery = this.getNewSearchQuery();
  }

  getNewSearchQuery() {
    return new SearchProductQuery('ProductName');
  }

  ngOnInit() {
    this.categories = this.categoriesService.getCategories();
    this.suppliers = this.suppliersService.getSuppliers();

    try {
      this.selectedItemId = +this.activatedRoute.firstChild.snapshot.paramMap.get('id');
      this.searchPanelCollapsed = true;
    } catch (e) {}

    //if we click on the Products in nav bar after a product has been opened
    this.routeSubscription = this.router.events.subscribe((event: any) => {
      if (event instanceof NavigationEnd) {
        if (event.urlAfterRedirects.split('/').length === 3) {
          this.searchPanelCollapsed = false;
        }
      }
    });
  }

  onSelectedIndexChange(event) {
    this.selectedItemId = event;
    if (this.autoCollapse)
      this.searchPanelCollapsed = true;
  }

  ngOnDestroy(): void {
    this.routeSubscription.unsubscribe();
  }

  onSortFieldChange(event) {
    if (this.searchProductQuery.sortField === event)
      this.searchProductQuery.isDesc = !this.searchProductQuery.isDesc;
    else
      this.searchProductQuery.sortField = event;

    this.search(false);
  }

  goToPage(pageIndex: number) {
    this.searchProductQuery.pageIndex = pageIndex;
    this.search(false);
  }

  reset() {
    this.searchProductQuery = this.getNewSearchQuery();
  }

  search(isFromUi: boolean) {

    let isEmpty = this.searchProductQuery.isEmptyQuery();
    if (isEmpty) {
      this.searchErrorMessage = "Please provide at least one search criteria.";
      return;
    }

    if (isFromUi)
      this.searchProductQuery.pageIndex = 0;
    this.productsService.getProducts(this.searchProductQuery).subscribe(x => {
      this.searchResult = x;
    });
  }
}