import { Component, OnInit, Input, Output, OnDestroy } from '@angular/core';
import { IProductListItem } from '../../../models/IProductSearchResponse';
import { GridField } from '../../../models/GridField';
import { EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductsService } from '../../../services/products.service';
import { EditProductQuery } from '../../../services/IEditProductQuery';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  gridFields: GridField[];
  selectedItemId: number;
  @Input() products: IProductListItem[] = [];
  @Output() sortFieldChange = new EventEmitter();
  @Output() selectedIndexChange = new EventEmitter();
  sortField: string;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private productsService: ProductsService,
  ) { }

  ngOnInit() {
    this.gridFields = [
      new GridField("ProductId", "Product Id", true),
      new GridField("ProductName", "Product Name", false),
      new GridField("SupplierId", "Supplier", false),
      new GridField("CategoryId", "Category", false),
      new GridField("QuantityPerUnit", "Quantity Per Unit", false),
      new GridField("UnitPrice", "Unit Price", false),
    ];

    this.sortField = this.gridFields[1].fieldName;
    this.productsService.editedProductbehaviorSubject.asObservable().subscribe((editedProduct: EditProductQuery) => {
      let editedProductInGrid = this.products.find(x => x.productId === editedProduct.productId);
      if (editedProductInGrid !== null) {
        let index = this.products.indexOf(editedProductInGrid);
        this.products[index] = editedProduct as IProductListItem;
      }
    });
  }

  emitSortField(event, isHidden) {

    if (isHidden)
      return;

    var target = event.target || event.srcElement || event.currentTarget;
    this.sortField = target.attributes.id.value;
    this.sortFieldChange.emit(this.sortField);
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
      this.selectedIndexChange.emit(item.productId);
    });
  }
}
