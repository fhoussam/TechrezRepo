import { Component, OnInit } from '@angular/core';
import { EditProductQuery } from '../../../services/IEditProductQuery';
import { ProductsService } from '../../../services/products.service';
import { CategoriesService } from '../../../services/categories.service';
import { SuppliersService } from '../../../services/suppliers.service';
import { Observable, timer } from 'rxjs';
import { switchMap, map } from 'rxjs/operators';
import { ISupplier } from '../../../models/ISupplier';
import { ICategory } from '../../../models/ICategory';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { firstValueMustBeGreaterThanSecondValueValidator } from '../../../custom-validators/firstValueMustBeGreaterThanSecondValueValidator';
import { shouldBeLessThanValidator } from '../../../custom-validators/shouldBeLessThanValidator';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css']
})
export class ProductEditComponent implements OnInit {

  editProductQuery : EditProductQuery;
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

    this.activatedRoute.paramMap.subscribe(x => {
      let id = +x.get('id');
      this.getProduct(id);
    });
  }

  getProduct(id: number) {
    this.productsService.getProduct(id).subscribe(y => {
      //we needed to add supplier id to source as the destination needs it
      this.editProductQuery = y as EditProductQuery;
      this.setFormValue(this.editProductQuery);
    });
  }

  private setFormValue(editProductQuery: EditProductQuery) {

    let productId = +this.activatedRoute.snapshot.params.id

    this.editForm = new FormGroup({
      'productName': new FormControl
        (
          editProductQuery.productName
          , [
            Validators.required,
            Validators.minLength(4),
            Validators.maxLength(100),
            Validators.pattern("^[A-Za-z'_ öä]*$")
          ]
          , this.isExistingProductName(productId).bind(this)
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

  saveChanges() {
    if (!this.editForm.valid)
      this.editForm.markAllAsTouched();
    else {

      this.editProductQuery.productName = this.editForm.get('productName').value;
      this.editProductQuery.supplierId = +this.editForm.get('supplierId').value;
      this.editProductQuery.categoryId = +this.editForm.get('categoryId').value;
      this.editProductQuery.quantityPerUnit = this.editForm.get('quantityPerUnit').value;
      this.editProductQuery.unitPrice = +this.editForm.get('unitPrice').value;
      this.editProductQuery.unitsInStock = +this.editForm.get('unitsInStock').value;
      this.editProductQuery.unitsOnOrder = +this.editForm.get('unitsOnOrder').value;
      this.editProductQuery.reorderLevel = +this.editForm.get('reorderLevel').value;
      this.editProductQuery.discontinued = this.editForm.get('discontinued').value;

      this.productsService.editProduct(this.editProductQuery).subscribe(x => {
        this.router.navigate(['../details'], { relativeTo: this.activatedRoute });
      });
    }
  }

  resetValues() {
    let id: number = +this.activatedRoute.snapshot.params.id;
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
