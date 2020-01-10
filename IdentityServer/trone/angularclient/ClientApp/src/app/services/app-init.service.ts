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
        return this.httpClient.get('https://localhost:44301/api/security/antiforgery').toPromise().then(x => {
            console.log('init anti forgery');

            //this.auth.getUserContext().subscribe(x => console.log('aaaa  ' + JSON.stringify(x)));


            return Promise.all([
                this.httpClient.get<category[]>('https://localhost:44301/api/product/categories').toPromise()
                    .then(settings => {
                        console.log('init app data');
                        APP_SETTINGS.categories = settings;
                    }),

                

                this.auth.getUserContext().toPromise().then(x => {
                    console.log('checking user context');
                }),
            ]);
        });
    }
}
