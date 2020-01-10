import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate  {

    constructor(
        private auth: AuthService,
    ) { }

    canActivate(
        route: ActivatedRouteSnapshot,
        router: RouterStateSnapshot
    ): boolean | Promise<boolean> | Observable<boolean> {

        const allowedRoles = route.data.allowedRoles;

        return this.auth.userContext.pipe(map(usercontext => {

            if (usercontext == null) {
                return false;
            }
            else {

                if (usercontext.authTime != null) {
                    let isAuthorized: boolean = this.isAuthorized(allowedRoles, usercontext.roles);
                    if (!isAuthorized) {
                        return false;
                    }
                    else {
                        //console.log('granting user to ' + route + ', roles : ' + JSON.stringify(usercontext.roles));
                        return true;
                    }
                }
                else {
                    //setTimeout(() => { this.auth.challengeOidc(router.url.toString()) }, 5000); 
                    this.auth.challengeOidc(router.url.toString());
                }
            }
        }));
    }

    private isAuthorized(allowedRoles: string[], userRoles: string[]): boolean {
        if (!allowedRoles || allowedRoles.length == 0) return true;
        let intersect: string[] = allowedRoles.filter(value => -1 !== userRoles.indexOf(value))
        return intersect.length != 0;
    }
}
