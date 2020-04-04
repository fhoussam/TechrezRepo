import { Component, OnInit, Output } from '@angular/core';
import { ProductsService } from '../../../services/products.service';
import { SuppliersService } from '../../../services/suppliers.service';
import { Observable, timer, BehaviorSubject } from 'rxjs';
import { switchMap, map } from 'rxjs/operators';
import { ISupplier } from '../../../models/ISupplier';
import { ICategory } from '../../../models/ICategory';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { firstValueMustBeGreaterThanSecondValueValidator } from '../../../custom-validators/firstValueMustBeGreaterThanSecondValueValidator';
import { shouldBeLessThanValidator } from '../../../custom-validators/shouldBeLessThanValidator';
import { CanCompoDeactivate } from '../../../guards/can-deactivate';
import { CategoriesService } from '../../../shared-module/services/categories.service';
import { EditProductQuery } from '../../../models/IEditProductQuery';
import { Store } from '@ngrx/store';
import { IAppState } from '../../../shared-module/reducers/shared-reducer-selector';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css']
})
export class ProductEditComponent implements OnInit, CanCompoDeactivate {

  editProductQuery: EditProductQuery;
  editProductQueryPreviousState: any;
  suppliers: Observable<ISupplier[]>;
  categories: Observable<ICategory[]>;
  editForm: FormGroup;
  saved: boolean;
  isAddMode: boolean;

  @Output() itemSaved = new EventEmitter();

  constructor(
    private productsService: ProductsService,
    private categoriesService: CategoriesService,
    private suppliersService: SuppliersService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private store: Store<IAppState>,
  ) { }

  ngOnInit() {
    this.suppliers = this.suppliersService.getSuppliers();
    this.categories = this.categoriesService.getCategories();
    this.saved = false;

    this.activatedRoute.paramMap.subscribe(x => {
      let id = +x.get('id');
      this.isAddMode = id === 0;
      this.getProduct(id);
    });
  }

  CanDeactivate(): boolean | Observable<boolean> | Promise<boolean> {
    if (this.saved)
      return true;
    else {
      let jsonPreviousState = JSON.stringify(this.editProductQueryPreviousState);
      let jsonFormState = JSON.stringify(this.editForm.value);
      let isDirty = jsonPreviousState !== jsonFormState;
      if (isDirty)
        return confirm('Do you want to discard the changes ?');
      else return true;
    }
  }

  getProduct(id: number) {
    this.productsService.getProduct(id).subscribe(y => {
      //we needed to add supplier id to source as the destination needs it
      this.editProductQuery = y as EditProductQuery;
      this.editForm = this.getNewFormGroupValue(this.editProductQuery);
      this.editProductQueryPreviousState = { ...this.editForm.value };
    });
  }

  private getNewFormGroupValue(editProductQuery: EditProductQuery) {

    let editForm = new FormGroup({
      'productId': new FormControl(editProductQuery.productId),
      'productName': new FormControl
        (
          editProductQuery.productName
          , [
            Validators.required,
            Validators.minLength(4),
            Validators.maxLength(100),
            Validators.pattern("^[A-Za-z'_ öä]*$")
          ]
          , this.isExistingProductName(editProductQuery.productId).bind(this)
        ),
      'supplierId': new FormControl(editProductQuery.supplierId, [Validators.required]),
      'categoryId': new FormControl(editProductQuery.categoryId, [Validators.required]),
      'quantityPerUnit': new FormControl(editProductQuery.quantityPerUnit, [Validators.required]),
      'unitPrice': new FormControl(editProductQuery.unitPrice, [Validators.required, this.isDividableByTenAndGreaterThanZero]),
      'unitsInStock': new FormControl(editProductQuery.unitsInStock, [Validators.required]),
      'unitsOnOrder': new FormControl(editProductQuery.unitsOnOrder, [Validators.required]),
      'reorderLevel': new FormControl(editProductQuery.reorderLevel, [Validators.required, shouldBeLessThanValidator(5)]),
      'discontinued': new FormControl(editProductQuery.discontinued, [Validators.required]),
    }
      , { validators: firstValueMustBeGreaterThanSecondValueValidator('unitsInStock', 'unitsOnOrder') }
    );

    return editForm;
  }

  isDividableByTenAndGreaterThanZero(c: FormControl) {

    //letting 'Required' take charge from here!
    if (c.value === null)
      return null;

    let result: boolean = +c.value % 10 == 0 && +c.value > 0;
    return result ? null : {
      dividableBy10: {
        valid: false
      }
    }
  }

  isExistingProductName =
    (productId: number, time: number = 500) => {
      return (input: FormControl) => {
        return timer(time).pipe(
          switchMap(() => this.productsService.isExistingProductName(input.value, productId)),
          map(result => {
            return !result ? null : { productNameAlreadyExists: true }
          })
        );
      };
    };

  getDeserializedFormGroupValue(): EditProductQuery {
    let formValue = new EditProductQuery();

    formValue.productId = this.editForm.get('productId').value;
    formValue.productName = this.editForm.get('productName').value;
    formValue.supplierId = +this.editForm.get('supplierId').value;
    formValue.categoryId = +this.editForm.get('categoryId').value;
    formValue.quantityPerUnit = this.editForm.get('quantityPerUnit').value;
    formValue.unitPrice = +this.editForm.get('unitPrice').value;
    formValue.unitsInStock = +this.editForm.get('unitsInStock').value;
    formValue.unitsOnOrder = +this.editForm.get('unitsOnOrder').value;
    formValue.reorderLevel = +this.editForm.get('reorderLevel').value;
    formValue.discontinued = this.editForm.get('discontinued').value;

    return formValue;
  }

  saveChanges() {
    if (!this.editForm.valid)
      this.editForm.markAllAsTouched();
    else {
      this.editProductQuery = this.getDeserializedFormGroupValue();
      this.productsService.editProduct(this.editProductQuery).subscribe(x => {
        this.saved = true;
        if (this.isAddMode) {
          let url = '/admin/products/' + x + '/details';
          this.itemSaved.emit(url);
          //this.router.navigateByUrl(url).then(() => {
            
          //});
        }
        else {
          this.router.navigate(['../details'], { relativeTo: this.activatedRoute }).then(x => {
            this.productsService.editedProductbehaviorSubject.next(this.editProductQuery);
          });
        }
      });
    }
  }

  resetValues() {
    let id: number = this.isAddMode ? +this.activatedRoute.snapshot.params.id : 0;
    this.getProduct(id);
  }
}

//async validator with no input param, we dont need that here for now
//isExistingProductName(control: FormControl): Promise<any> | Observable<any> {
//  const promise = new Promise<any>((resolve, reject) => {
//    this.productsService.isExistingProductName(control.value).subscribe(x => {
//      if (x) resolve({ 'productNameAlreadyExists': true });
//      else resolve(null);
//    });
//  });
//  return promise;
//}
