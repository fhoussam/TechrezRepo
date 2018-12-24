import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';
import { product } from 'src/app/models/product';
import { productFilter } from 'src/app/models/productfilter';
import { category } from 'src/app/models/category';
import { APP_SETTINGS } from 'src/app/models/APP_SETTINGS';

@Component({
  selector: 'app-productlist',
  templateUrl: './productlist.component.html',
  styleUrls: ['./productlist.component.css']
})
export class ProductlistComponent implements OnInit {

  constructor(private productService:ProductService) { }
  products : product[] = [];
  pageCount : number;
  productFilter : productFilter;
  selecedPage : number;
  pageSizes:number[];
  propNames:string[];
  selectedProduct:product;
  categories:category[];
  editMode:boolean;

  ngOnInit() {
    this.pageSizes = [ 5, 10, 15 ];
    this.productFilter = new productFilter();
    this.getProducts();
    this.categories = APP_SETTINGS.categories;
    this.editMode = false;
  }

  refreshDetailsSection(nonUpdatedProduct:product)
  {
    //just getting the old values from the list
    this.selectedProduct = this.products.filter(x=> x.id == nonUpdatedProduct.id)[0];
    this.editMode = false;
  }

  refreshUpdatedProduct(updatedProduct:product){
    //a copy by prop here is done on purpose as the edit product api response dto is not necesserly is same as grid item data model
    let oldProductValues = this.products.filter(x=> x.id == updatedProduct.id)[0];
    oldProductValues.categoryID = updatedProduct.categoryID;
    oldProductValues.description = updatedProduct.description;
    oldProductValues.price = updatedProduct.price;
    oldProductValues.stock = updatedProduct.stock;
    this.selectedProduct = oldProductValues;
    this.editMode = false;
  }

  getProducts(){
    return this.productService.getProducts(this.productFilter).subscribe(data => 
      { 
        this.products = data.pageData;
        this.pageCount = data.count / this.productFilter.pageSize;
        if(data.count / this.products.length > Math.floor(this.pageCount)) {
          this.pageCount = Math.floor(this.pageCount) + 1; 
        }
        this.propNames = ['Description','Stock','Price','Category'];

        //when you call the api the selections gets lost as the objects references on client side are no longer
        //as a result we need to look at the selected object within the current api search result 
        if(this.selectedProduct != null){
          let p = this.products.filter(x=> x.id == this.selectedProduct.id)[0];
          if(p != null){
            this.selectedProduct = p;
          }
        }
        // this.selectedProduct = this.products [0];
      });
  }

  filterProductList(){
    this.getProducts();
  }

  initFilter(){
    this.productFilter = new productFilter();
    this.getProducts();
  }

  selectPage(selectedIndex)
  {
    this.productFilter.pageIndex = selectedIndex;
    this.getProducts();
  }

  changePageSize()
  {
    this.productFilter.pageIndex = 0;
    this.getProducts();
  }

  sort(sortColumn)
  {
    if(this.productFilter.orderColumn == sortColumn)
      this.productFilter.isDesc = !this.productFilter.isDesc;
    else
      this.productFilter.orderColumn = sortColumn;
    this.getProducts(); 
  }

  selectProduct(p:product){
    if(!this.editMode)
      this.selectedProduct = p;
  }

  deleteProduct(productid:number){
    this.productService.deleteProduct(productid).subscribe(data => { 
      console.log('product deleted');
      this.getProducts();
    });
  }

  createNewProduct(){
    
  }
}
