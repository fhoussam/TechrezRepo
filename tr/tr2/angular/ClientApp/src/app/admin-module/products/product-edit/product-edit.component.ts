import { Component, OnInit } from '@angular/core';
import { EditProductQuery } from '../../../services/IEditProductQuery';
import { ProductsService } from '../../../services/products.service';
import { CategoriesService } from '../../../services/categories.service';
import { SuppliersService } from '../../../services/suppliers.service';
import { Observable } from 'rxjs';
import { ISupplier } from '../../../models/ISupplier';
import { ICategory } from '../../../models/ICategory';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css']
})
export class ProductEditComponent implements OnInit {

  editProductQuery = new EditProductQuery();
  suppliers: Observable<ISupplier[]>;
  categories: Observable<ICategory[]>;

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

  resetValues() {
    let id: number = +this.activatedRoute.snapshot.params.id;
    this.getProduct(id);
  }

  getProduct(id: number) {
    this.productsService.getProduct(id).subscribe(y => {
      //we needed to add supplier id to source as the destination needs it
      this.editProductQuery = y as EditProductQuery;
    });
  }

  saveChanges() {
    this.productsService.editProduct(this.editProductQuery).subscribe(x => {
      this.router.navigate(['../details'], { relativeTo: this.activatedRoute });
    });
  }
}
