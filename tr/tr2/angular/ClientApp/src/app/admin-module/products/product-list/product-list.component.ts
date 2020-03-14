import { Component, OnInit, Input } from '@angular/core';
import { IProductSearchResponse, IProductListItem } from '../../../models/IProductSearchResponse';
import { GridField } from '../../../models/GridField';
import { SearchQueryExtension } from '../../../models/ISearchQueryExtension';
import { SearchQueryExtensionEmitterService } from '../../../services/search-query-extension-emitter.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  @Input() searchResult: IProductSearchResponse;
  selectedItem: IProductListItem;
  searchQueryExtension = new SearchQueryExtension();
  gridFields: GridField[];

  constructor(private searchQueryExtensionEmitterService: SearchQueryExtensionEmitterService) {

    this.gridFields = [
      new GridField("ProductId", "Product Id", true),
      new GridField("ProductName", "Product Name", false),
      new GridField("SupplierId", "Supplier", false),
      new GridField("CategoryId", "Category", false),
      new GridField("QuantityPerUnit", "Quantity Per Unit", false),
      new GridField("UnitPrice", "Unit Price", false),
    ];

    this.searchQueryExtension.isDesc = false;
    this.searchQueryExtension.pageIndex = 0;
    this.searchQueryExtension.sortField = this.gridFields[1].fieldName;
  }

  ngOnInit() { }

  emitPageIndex(pageIndex: number) {
    this.searchQueryExtension.pageIndex = pageIndex;
    this.searchQueryExtensionEmitterService.emit(this.searchQueryExtension);
  }

  emitSortField(event, isHidden) {

    if (isHidden)
      return;

    var target = event.target || event.srcElement || event.currentTarget;
    var idAttr = target.attributes.id.value;

    if (this.searchQueryExtension.sortField == idAttr)
      this.searchQueryExtension.isDesc = !this.searchQueryExtension.isDesc;
    else
      this.searchQueryExtension.sortField = idAttr;

    this.searchQueryExtensionEmitterService.emit(this.searchQueryExtension);
  }

  selectItem(item) {
    this.selectedItem = item;
    //navigate to selected tab or just to the id
    //on success of navigate, emit selected item to update changes back
  }
}
