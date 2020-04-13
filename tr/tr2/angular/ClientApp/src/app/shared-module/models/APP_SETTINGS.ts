import { ICategory } from '../../models/ICategory';
export class APP_SETTINGS {
  static categories: ICategory[];
  static baseUrl = 'api/';
  static queryStringDateFormat: 'yyyy-MM-dd hh:mm:ss ZZZZZ';
}
