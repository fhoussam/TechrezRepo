import { Component, OnInit } from '@angular/core';
import { EditProductQuery } from '../../../services/IEditProductQuery';
import { ProductsService } from '../../../services/products.service';
import { CategoriesService } from '../../../services/categories.service';
import { SuppliersService } from '../../../services/suppliers.service';
import { Observable } from 'rxjs';
import { ISupplier } from '../../../models/ISupplier';
import { ICategory } from '../../../models/ICategory';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { SearchProductQuery } from '../../../models/SearchProductQuery';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css']
})
export class ProductEditComponent implements OnInit {

  editProductQuery = new EditProductQuery();
  suppliers: Observable<ISupplier[]>;
  categories: Observable<ICategory[]>;
  editForm: FormGroup;

  constructor(
    private productsService: ProductsService,
    private categoriesService: CategoriesService,
    private suppliersService: SuppliersService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
  ) { }

  ngOnInit() {
    this.suppliers = this.suppliersService.getSuppliers();
    this.categories = this.categoriesService.getCategories();

    //this.activatedRoute.paramMap.subscribe(x => {
    //  let id = +x.get('id');
    //  this.getProduct(id);
    //});
    this.setFormValue(new EditProductQuery());
  }

  getProduct(id: number) {
    this.productsService.getProduct(id).subscribe(y => {
      //we needed to add supplier id to source as the destination needs it
      this.editProductQuery = y as EditProductQuery;
      this.setFormValue(this.editProductQuery);
    });
  }

  private setFormValue(editProductQuery: EditProductQuery) {
    this.editForm = new FormGroup({
      'productName': new FormControl
        (
          editProductQuery.productName
          , [
            Validators.required,
            Validators.minLength(5),
            Validators.maxLength(100),
            Validators.pattern("^[A-Za-z_ öä]*$")
          ]
          ,this.isExistingProductName.bind(this)
        ),
      'supplierId': new FormControl(editProductQuery.supplierId, [Validators.required]),
      'categoryId': new FormControl(editProductQuery.categoryId, [Validators.required]),
      'quantityPerUnit': new FormControl(editProductQuery.quantityPerUnit, [Validators.required]),
      'unitPrice': new FormControl(editProductQuery.unitPrice, [Validators.required, this.isDividableByTenAndGreaterThanZero.bind(this)]),
      'unitsInStock': new FormControl(editProductQuery.unitsInStock, [Validators.required]),
      'unitsOnOrder': new FormControl(editProductQuery.unitsOnOrder, [Validators.required]),
      'reorderLevel': new FormControl(editProductQuery.reorderLevel, [Validators.required]),
      'discontinued': new FormControl(editProductQuery.discontinued, [Validators.required]),
    });
  }

  isDividableByTenAndGreaterThanZero(c: FormControl) {
    let result: boolean = +c.value % 10 == 0 && +c.value > 0;
    return result ? null : {
      dividableBy10: {
        valid: false
      }
    }
  }

  isExistingProductName(control: FormControl): Promise<any> | Observable<any> {
    const promise = new Promise<any>((resolve, reject) => {
      this.productsService.isExistingProductName(control.value).subscribe(x => {
        if (x) resolve({ 'productNameAlreadyExists': true });
        else resolve(null);
      });
    });
    return promise;
  }

  saveChanges() {
    console.log(this.editForm.get('unitPrice').errors);
    //this.productsService.editProduct(this.editProductQuery).subscribe(x => {
    //  this.router.navigate(['../details'], { relativeTo: this.activatedRoute });
    //});
  }

  resetValues() {
    let id: number = +this.activatedRoute.snapshot.params.id;
    this.getProduct(id);
  }
}
