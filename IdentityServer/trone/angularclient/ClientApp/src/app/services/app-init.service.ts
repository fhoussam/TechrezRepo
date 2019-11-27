import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { category } from '../models/category';
import { APP_SETTINGS } from '../models/APP_SETTINGS';

@Injectable({
  providedIn: 'root'
})
export class AppInitService {

    constructor(private httpClient: HttpClient) { }

    getSettings(): Promise<any> {
        const promise = this.httpClient.get<category[]>('http://localhost:5001/api/product/categories')
            .toPromise()
            .then(settings => {
                APP_SETTINGS.categories = settings;
                return settings;
            });

        return promise;
    }
}
