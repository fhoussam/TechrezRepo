import { Pipe, PipeTransform } from '@angular/core';
import { APP_SETTINGS } from '../models/APP_SETTINGS';

@Pipe({
  name: 'category'
})
export class CategoryPipe implements PipeTransform {

  transform(value: any, ...args: any[]): any {
      return APP_SETTINGS.categories.filter(x => x.code == value)[0].description;
  }
}
