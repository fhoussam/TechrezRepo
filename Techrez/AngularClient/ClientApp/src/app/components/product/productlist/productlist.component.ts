import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';
import { product } from 'src/app/models/product';
import { productFilter } from 'src/app/models/productfilter';
import { category } from 'src/app/models/category';
import { APP_SETTINGS } from 'src/app/models/APP_SETTINGS';
import { PageEvent, MatDialog } from '@angular/material';
import { ProductcreateComponent } from '../productcreate/productcreate.component';

@Component({
  selector: 'app-productlist',
  templateUrl: './productlist.component.html',
  styleUrls: ['./productlist.component.css']
})
export class ProductlistComponent implements OnInit {

  constructor(private productService: ProductService, public dialog: MatDialog) { }

  products: product[] = [];
  productFilter: productFilter;
  selecedPage: number;
  pageSizeOptions: number[];
  propNames: string[];
  selectedProduct: product;
  categories: category[];
  editMode: boolean;
  totalCount: number;

  public productToAdd = new product();
  openDialog(): void {
    const dialogRef = this.dialog.open(ProductcreateComponent, {
      width: '400px',
      data: this.productToAdd
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
      this.getProducts();
    });
  }

  ngOnInit() {
    this.pageSizeOptions = APP_SETTINGS.pageSizeOptions;
    this.productFilter = new productFilter();
    this.getProducts();
    this.categories = APP_SETTINGS.categories;
    this.editMode = false;
  }

  refreshDetailsSection(nonUpdatedProduct: product) {
    //just getting the old values from the list
    this.selectedProduct = this.products.filter(x => x.id == nonUpdatedProduct.id)[0];
    this.editMode = false;
  }

  refreshUpdatedProduct(updatedProduct: product) {
    //a copy by prop here is done on purpose as the edit product api response dto is not necesserly is same as grid item data model
    let oldProductValues = this.products.filter(x => x.id == updatedProduct.id)[0];
    //we dont really need to check if null as we disable item selection when edit mode is enabled
    oldProductValues.categoryID = updatedProduct.categoryID;
    oldProductValues.description = updatedProduct.description;
    oldProductValues.price = updatedProduct.price;
    oldProductValues.stock = updatedProduct.stock;
    this.selectedProduct = oldProductValues;
    this.editMode = false;
  }

  getProducts() {
    return this.productService.getProducts(this.productFilter).subscribe(data => {
      this.products = data.pageData;
      this.totalCount = data.count;
      this.propNames = ['description', 'stock', 'price', 'categoryID', 'action'];

      //when you call the api the selections gets lost as the objects references on client side are no longer
      //as a result we need to look at the selected object within the current api search result 
      if (this.selectedProduct != null) {
        let p = this.products.filter(x => x.id == this.selectedProduct.id)[0];
        if (p != null) {
          this.selectedProduct = p;
        }
      }

      // this.selectedProduct = this.products [0];
    });
  }

  filterProductList() {
    this.getProducts();
  }

  initFilter() {
    this.productFilter = new productFilter();
    this.getProducts();
  }

  page(pagingEvent: PageEvent)
  {
    this.productFilter.pageSize = pagingEvent.pageSize;
    this.productFilter.pageIndex = pagingEvent.pageIndex;
    this.getProducts();
  }

  sort(sortColumn) {
    if (this.productFilter.orderColumn == sortColumn)
      this.productFilter.isDesc = !this.productFilter.isDesc;
    else
      this.productFilter.orderColumn = sortColumn;
    this.getProducts();
  }

  selectProduct(p: product) {
    if (!this.editMode)
      this.selectedProduct = p;
  }

  deleteProduct(productid: number) {
    this.productService.deleteProduct(productid).subscribe(data => {
      console.log('product deleted');
      this.getProducts();
    });
  }

  createNewProduct() {

  }
}
