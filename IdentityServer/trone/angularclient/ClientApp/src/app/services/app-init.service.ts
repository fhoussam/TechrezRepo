import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { category } from '../models/category';
import { APP_SETTINGS } from '../models/APP_SETTINGS';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AppInitService {

    constructor(
        private httpClient: HttpClient,
        private auth: AuthService,
    ) { }

    getSettings(): Promise<any> {

        const promise_categories = this.httpClient.get<category[]>('https://localhost:44301/api/product/categories').toPromise()
            .then(settings => {
                console.log('init app data');
                APP_SETTINGS.categories = settings;
            })

        const promise_antiforgery = this.httpClient.get('https://localhost:44301/api/security/antiforgery').toPromise()
            .then(x => console.log('init anti forgery')
            );

        const promise_usercontext = this.auth.getUserContext().toPromise().then(x => {
            console.log('checking user context');
        });

        return promise_antiforgery.then(x => Promise.all([promise_categories, promise_usercontext]));
    }
}
