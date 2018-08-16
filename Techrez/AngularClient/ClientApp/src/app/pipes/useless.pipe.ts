import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'useless'
})
export class UselessPipe implements PipeTransform {

  transform(value: any, args?: any): any {
    return value;
  }

}
