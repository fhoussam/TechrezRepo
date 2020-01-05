import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { IUserContext } from '../models/userContext';
import { Observable, Subject, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    userContext = new BehaviorSubject<IUserContext>(null);

    constructor(
        private http: HttpClient,
        private injector: Injector, //in case someday you struggle with a cyclic dependency issue
        //private router: Router,
    ) { }

    public isAuthenticated(): boolean {
        return true;
    }

    public challengeOidc() {
        let router = this.injector.get(Router);
        window.location.replace("https://localhost:44301/api/security/challengeoidc?returnUrl=" + router.url);
        //window.location.replace("https://localhost:44301/api/security/challengeoidc?returnUrl=" + this.router.url);
    }

    public getUserContext():Observable<IUserContext> {
        return this.http.get<IUserContext>('https://localhost:44301/api/security/usercontext')
            .pipe(tap(resData => {
                this.userContext.next(resData);
            }));
    }
}
