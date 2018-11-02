import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs/Observable' 
import { Category } from '../Models/Category';

@Injectable()
export class CaregoryService
{
  constructor(private httpClient: HttpClient) { }

  public getCategories():Observable<Category[]>
  {
    return this.httpClient.get<Category[]>("https://localhost:44356/api/categories");
  }
}
