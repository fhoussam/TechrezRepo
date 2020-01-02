import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { category } from '../models/category';
import { APP_SETTINGS } from '../models/APP_SETTINGS';

@Injectable({
  providedIn: 'root'
})
export class AppInitService {

    constructor(
        private httpClient: HttpClient,
    ) { }

    getSettings(): Promise<any> {

        const promise_categories = this.httpClient.get<category[]>('https://localhost:44301/api/product/categories').toPromise()
            .then(settings => APP_SETTINGS.categories = settings);

        const promise_antiforgery = this.httpClient.get('https://localhost:44301/api/antiforgery');

        return Promise.all([promise_categories, promise_antiforgery]);
    }
}
