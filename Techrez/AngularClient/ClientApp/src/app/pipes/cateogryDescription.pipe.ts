import { Pipe, PipeTransform } from '@angular/core';
import { CaregoryService } from '../services/categories.service';
import { List } from 'linqts';
import { Category } from '../Models/Category';

@Pipe({
  name: 'CateogryDescription'
})
export class CateogryDescriptionPipe implements PipeTransform {

  categories: Category[] = [];
  constructor(private caregoryService: CaregoryService){
    if (this.categories.length == 0) this.caregoryService.getCategories().subscribe(data => { this.categories = data; });
  }

  transform(value: any, args?: any): any {
    return this.categories.filter(item => item.id == value)[0].description;;
  }

}
