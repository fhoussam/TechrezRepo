import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRouteSnapshot } from '@angular/router';
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
    ) { }

    public challengeOidc(returnUrl: string) {
        window.location.replace("https://localhost:44301/api/security/challengeoidc?returnUrl=" + returnUrl);
    }

    public getUserContext():Observable<IUserContext> {
        return this.http.get<IUserContext>('https://localhost:44301/api/security/usercontext')
            .pipe(tap(resData => {
                this.userContext.next(resData);
            }));
    }

    public logout() {
        window.location.replace("https://localhost:44301/api/security/logout");
    }
}
