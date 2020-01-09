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
            if (!!usercontext && this.isAuthorized(allowedRoles, usercontext.roles))
                return true;
            else
                this.auth.challengeOidc(router.url.toString());
        }));
    }

    private isAuthorized(allowedRoles: string[], userRoles: string[]): boolean {
        if (!allowedRoles || allowedRoles.length == 0) return true;
        let intersect: string[] = allowedRoles.filter(value => -1 !== userRoles.indexOf(value))
        return intersect.length != 0;
    }
}
