import { Component, OnInit, Input, Output } from '@angular/core';
import { SearchProductQueryResponse } from '../../../models/IProductSearchResponse';
import { GridField } from '../../../models/GridField';
import { EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductsService } from '../../../services/products.service';
import { List } from 'linqts';
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
  @Output() selectionChange = new EventEmitter();
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

  getTrBackGroundColor(item: SearchProductQueryResponse): string {

    //selected for open
    if (item.productId === this.selectedItemId) {
      return '#b5dcf1';
    }

    //selected for delete
    else if (item.selected) {
      return '#ffbaba';
    }

    else
      return 'inherit';
  }

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private productsService: ProductsService,
  ) { }

  toggleSelectAll($event) {
    this.products.forEach((x) => x.selected = $event.target.value);
  }

  isAllSelected() {
    new List<SearchProductQueryResponse>(this.products).All(x => x.selected);
  }

  //toggleSelect(product: SearchProductQueryResponse, $event: any) {
  //  console.log($event);
  //  product.selected = $event.target.value;
  //}

  ngOnInit() {
    this.gridFields = [
      new GridField("ProductId", "Product Id", 0),
      new GridField("ProductName", "Product Name", 1),
      new GridField("SupplierId", "Supplier", 2),
      new GridField("CategoryId", "Category", 3),
      new GridField("QuantityPerUnit", "Quantity Per Unit", 4),
      new GridField("UnitPrice", "Unit Price", 5),
    ];

    this.sortFieldIndex = 1;
    
    //listen when an update is performed on a displayed entity
    this.productsService.editedProductbehaviorSubject.asObservable().subscribe((editedProduct: EditProductQuery) => {
      if (editedProduct) {
        const editedProductInGrid = this.products.find(x => x.productId === editedProduct.productId);
        if (editedProductInGrid !== null) {
          const index = this.products.indexOf(editedProductInGrid);
          this.products[index] = { ...editedProduct, selected: this.products[index].selected };
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

  openItem(item: SearchProductQueryResponse) {

    //not allowing an item to be deleted when its opened
    item.selected = false;

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
