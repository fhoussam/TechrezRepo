import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http'
import { Observable, BehaviorSubject, of } from 'rxjs';
import { SearchProductQueryResponse } from '../models/IProductSearchResponse';
import { SearchProductQuery } from '../models/SearchProductQuery';
import { IProductDetails } from '../models/IProductDetails';
import { EditProductQuery } from '../models/IEditProductQuery';
import { PagedList } from '../models/PagedList';
import { HttpHelperService } from '../shared-module/services/http-helper';
import { APP_SETTINGS } from '../shared-module/models/APP_SETTINGS';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  readonly baseUrl = APP_SETTINGS.baseUrl + "products";
  editedProductbehaviorSubject = new BehaviorSubject(new EditProductQuery());

  constructor(private http: HttpClient, private httpHelper: HttpHelperService) { }

  public getProducts(searchProductQuery: SearchProductQuery): Observable<PagedList<SearchProductQueryResponse>> {
    return this.http.get<PagedList<SearchProductQueryResponse>>(this.baseUrl, {
      params: this.httpHelper.toHttpParams(searchProductQuery)
    });
  }

  public getProduct(productId: number): Observable<IProductDetails> {
    if (productId > 0) {
      return this.http.get<IProductDetails>(this.baseUrl + '/' + productId);
    }
    else {
      const fakeObject: IProductDetails = {
        productName: "Random Product one",
        supplierId: 4,
        categoryId: 4,
        quantityPerUnit: "10 - 500 g pkgs.",
        unitPrice: 40,
        unitsInStock: 40,
        unitsOnOrder: 4,
        discontinued: false,
        productId: 0,
        reorderLevel: 4,
        supplier: null,
      };
      //fakeObject = {} as IProductDetails;
      return of(fakeObject);
    }
  }

  public editProduct(editProductQuery: EditProductQuery) {
    return this.http.post(this.baseUrl, editProductQuery);
  }

  public isExistingProductName(productName: string, productId: number): Observable<boolean> {
    return this.http.get<boolean>(this.baseUrl + '/IsExistingProductName', {
      params: this.httpHelper.toHttpParams({
        productName: productName,
        productId: productId
      })
    });
  }
}
