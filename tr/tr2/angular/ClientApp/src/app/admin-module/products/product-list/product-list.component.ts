import { Component, OnInit, Input, Output, OnDestroy } from '@angular/core';
import { SearchProductQueryResponse } from '../../../models/IProductSearchResponse';
import { GridField } from '../../../models/GridField';
import { EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductsService } from '../../../services/products.service';
import { EditProductQuery } from '../../../models/IEditProductQuery';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  gridFields: GridField[];
  selectedItemId: number;
  @Output() sortFieldChange = new EventEmitter();
  @Output() selectedIndexChange = new EventEmitter();
  sortFieldIndex: number;

  _products: SearchProductQueryResponse[];
  @Input()
  set products(products: SearchProductQueryResponse[]) {
    if (products) {
      this._products = products;
      try {
        //if the user gains direct access to an element and perform a search afterwards,
        //that elemnt should be highlighted on the result 
        this.selectedItemId = +this.activatedRoute.firstChild.snapshot.paramMap.get('id');
      } catch (e) {}
    }
  }
  get products() {
    return this._products;
  }

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private productsService: ProductsService,
  ) { }

  ngOnInit() {
    this.gridFields = [
      new GridField("ProductId", "Product Id", 0, true),
      new GridField("ProductName", "Product Name", 1, false),
      new GridField("SupplierId", "Supplier", 2, false),
      new GridField("CategoryId", "Category", 3, false),
      new GridField("QuantityPerUnit", "Quantity Per Unit", 4, false),
      new GridField("UnitPrice", "Unit Price", 5, false),
    ];

    this.sortFieldIndex = 1;
    
    //listen when an update is performed on a displayed entity
    this.productsService.editedProductbehaviorSubject.asObservable().subscribe((editedProduct: EditProductQuery) => {
      if (editedProduct) {
        const editedProductInGrid = this.products.find(x => x.productId === editedProduct.productId);
        if (editedProductInGrid !== null) {
          const index = this.products.indexOf(editedProductInGrid);
          this.products[index] = editedProduct as SearchProductQueryResponse;
        }
      }
      else
        this.selectedItemId = null;
    });
  }

  emitSortField(event, isHidden) {

    if (isHidden)
      return;

    const target = event.target || event.srcElement || event.currentTarget;
    this.sortFieldIndex = +target.getAttribute('data-sortfieldIndex');
    this.sortFieldChange.emit(this.sortFieldIndex);
  }

  selectItem(item: SearchProductQueryResponse) {

    this.selectedItemId = item.productId;

    let selectedTab: string;
    try {
      selectedTab = this.activatedRoute.snapshot.firstChild.routeConfig.path.split('/')[1];
    } catch (e) {
      selectedTab = 'details';
    }

    this.router.navigate([item.productId + '/' + selectedTab], { relativeTo: this.activatedRoute }).then(x => {
      this.selectedIndexChange.emit(item.productId);
    });
  }
}
