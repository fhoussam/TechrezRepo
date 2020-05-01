import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { SearchProductQueryResponse } from '../../../models/IProductSearchResponse';
import { ProductsService } from '../../../services/products.service';
import { SearchProductQuery } from '../../../models/SearchProductQuery';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { Subscription } from 'rxjs';
import { Store } from '@ngrx/store';
import { APP_SETTINGS } from '../../../shared-module/models/APP_SETTINGS';
import { IAppState } from '../../../shared-module/reducers/shared-reducer-selector';
import { AlertAction, ConfirmAction } from '../../../shared-module/reducers/spiner-reducer/spiner-actions';
import { PagedList } from '../../../models/PagedList';
import { DdlKeyValue, DropDownListIdentifier } from '../../../models/config-models';
import { tap } from 'rxjs/operators';
import { List } from 'linqts';

@Component({
  selector: 'app-product-search',
  templateUrl: './product-search.component.html',
  styleUrls: ['./product-search.component.css']
})
export class ProductSearchComponent implements OnInit, OnDestroy {

  searchResult: PagedList<SearchProductQueryResponse>;
  routeSubscription: Subscription;
  categories: DdlKeyValue[];
  suppliers: DdlKeyValue[];
  searchProductQuery: SearchProductQuery;
  @ViewChild('f', { static: false }) searchForm: NgForm;
  searchPanelCollapsed: boolean;
  autoCollapse: boolean;
  selectedItemId: number;
  isAddMode: boolean;

  collapse() {
    this.searchPanelCollapsed = !this.searchPanelCollapsed;
  }

  constructor(
    private productsService: ProductsService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private store: Store<IAppState>
  ) {
    this.searchPanelCollapsed = false;
    this.searchProductQuery = this.getNewSearchQuery();
    this.searchProductQuery.discontinued = false;
  }

  emptyDeleteSelection() {
    this.searchResult.source.forEach((x)=> x.selected = false);
  }

  isExistingSelectedElements(): boolean {
    return !new List<SearchProductQueryResponse>(this.searchResult.source).Any(x => x.selected);
  }

  delete() {
    const ids = new List<SearchProductQueryResponse>(this.searchResult.source).Where(x=>x.selected).Select(x=>x.productId).ToArray();
    this.store.dispatch(new ConfirmAction({
      messageValue: "Sure you wanna delete these " + ids.length + " item(s) ?",
      yesAsyncCallback:
        this.productsService.delete(ids).pipe(
          tap(() => { this.search(false); })
        )
    }));
  }

  showAddForm(event) {
    event.stopPropagation();
    this.router.navigateByUrl('/admin/products/new').then(x => {
      this.isAddMode = true;
      this.searchPanelCollapsed = false;
      this.selectedItemId = null;
    });
  }

  getNewSearchQuery() {
    return new SearchProductQuery();
  }

  ngOnInit() {

    this.categories = APP_SETTINGS.categories;
    this.productsService.getFormData().subscribe(formdata => {
      this.suppliers = formdata[DropDownListIdentifier.Suppliers];
    });

    try {
      const childRoute = this.activatedRoute.firstChild;
      if (childRoute.snapshot.url[0].path === 'new')
        this.isAddMode = true;
      else {
        this.selectedItemId = +childRoute.snapshot.paramMap.get('id');
        this.searchPanelCollapsed = true;
      }
    } catch (e) { }
    
    this.routeSubscription = this.router.events.subscribe((event: any) => {
      if (event instanceof NavigationEnd) {
        const urlParts = event.urlAfterRedirects.split('/');
        console.log(urlParts);
        console.log(urlParts.length);
        //if we click on the Products in nav bar after a product has been opened
        if (urlParts.length === 3) {
          this.searchPanelCollapsed = false;
          this.selectedItemId = null;
        }
        else if (urlParts.length > 3 && urlParts[3] !== 'new') {
          this.selectedItemId = +urlParts[3];
        }
      }
    });

    this.search(true);
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
    if (this.searchProductQuery.sortFieldIndex === event)
      this.searchProductQuery.isDesc = !this.searchProductQuery.isDesc;
    else
      this.searchProductQuery.sortFieldIndex = event;

    this.search(false);
  }

  goToPage(pageIndex: number) {
    this.searchProductQuery.pageIndex = pageIndex;
    this.search(false);
  }

  reset() {
    this.searchProductQuery = this.getNewSearchQuery();
  }

  search(withInitPageIndex: boolean) {
    const isEmpty = this.searchProductQuery.isEmptyQuery();
    if (isEmpty) {
      this.store.dispatch(new AlertAction("Please provide at least one search criteria."));
    }
    else {
      if (withInitPageIndex)
        this.searchProductQuery.pageIndex = 0;

      this.productsService.getProducts(this.searchProductQuery).subscribe(x => {
        this.searchResult = x;
        if(this.searchResult.source.length > 0)
          this.searchResult.source.forEach((x)=>x.selected = false);
      });
    }
  }
}
