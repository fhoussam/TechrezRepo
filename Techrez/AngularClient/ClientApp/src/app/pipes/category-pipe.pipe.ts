import { Pipe, PipeTransform } from '@angular/core';
import { APP_SETTINGS } from '../models/APP_SETTINGS';

@Pipe({
  name: 'categoryPipe'
})
export class CategoryPipePipe implements PipeTransform {

  constructor()
  {
    
  }

  transform(value: number, args?: any): string {
    return APP_SETTINGS.categories.filter(x=>x.id == value)[0].description;
  }

}
