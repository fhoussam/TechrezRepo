import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { APP_SETTINGS } from '../models/APP_SETTINGS';
import { category } from '../models/category';

@Injectable()
export class AppLoadService {

  constructor(private httpClient: HttpClient) { }

  getSettings(): Promise<any> {
    const promise = this.httpClient.get<category[]>('https://localhost:44356/api/categories')
      .toPromise()
      .then(settings => {
        APP_SETTINGS.categories = settings;
        return settings;
      });

    return promise;
  }
}
