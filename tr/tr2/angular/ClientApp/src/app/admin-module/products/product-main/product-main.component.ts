import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { IProductSearchResponse, IProductListItem } from '../../../models/IProductSearchResponse';
import { GridField } from '../../../models/GridField';
import { SuppliersService } from '../../../services/suppliers.service';
import { CategoriesService } from '../../../services/categories.service';
import { ProductsService } from '../../../services/products.service';
import { SearchProductQuery } from '../../../models/IProductSearchQuery';
import { NgForm } from '@angular/forms';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-product-main',
  templateUrl: './product-main.component.html',
  styleUrls: ['./product-main.component.css']
})
export class ProductMainComponent implements OnInit, OnDestroy {

  searchResult: IProductSearchResponse;
  gridFields: GridField[];
  categories: any;
  suppliers: any;
  searchProductQuery = new SearchProductQuery();
  @ViewChild('f', { static: false }) searchForm: NgForm;
  //selectedItem: IProductListItem;
  selectedItemId: number;
  searchPanelCollapsed: boolean;
  routeSubscription: Subscription;
  autoCollapse: boolean;

  collapse() {
    this.searchPanelCollapsed = !this.searchPanelCollapsed;
  }

  constructor(
    private productsService: ProductsService,
    private categoriesService: CategoriesService,
    private suppliersService: SuppliersService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
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

    this.searchPanelCollapsed = false;
  }

  ngOnInit() {
    this.categories = this.categoriesService.getCategories();
    this.suppliers = this.suppliersService.getSuppliers();

    try {
      this.selectedItemId = this.activatedRoute.snapshot.firstChild.params.id;
      this.searchPanelCollapsed = true;
    } catch (e) {}

    //if we click on the Products in nav bar after a product has been opened
    this.routeSubscription = this.router.events.subscribe((event: any) => {
      if (event instanceof NavigationEnd) {
        if (event.urlAfterRedirects.split('/').length === 3) {
          this.selectedItemId = null;
          this.searchPanelCollapsed = false;
        }
      }
    });
  }

  ngOnDestroy(): void {
    this.routeSubscription.unsubscribe();
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

  selectItem(item: IProductListItem) {

    this.selectedItemId = item.productId;

    let selectedTab: string;
    try {
      selectedTab = this.activatedRoute.snapshot.firstChild.routeConfig.path.split('/')[1];
    } catch (e) {
      selectedTab = 'details';
    }

    this.router.navigate([item.productId + '/' + selectedTab], { relativeTo: this.activatedRoute }).then(x => {
      if (!this.searchPanelCollapsed && this.autoCollapse)
        this.searchPanelCollapsed = true;
    });
  }
}
