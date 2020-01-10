import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit, OnDestroy {

    private userContextSubscribtion: Subscription;
    isAuthenticated = false;
    roles : string[]

    constructor(
        private auth: AuthService,
        private router: Router,
    ) {

    }

    ngOnInit() {
        this.userContextSubscribtion = this.auth.userContext.subscribe(userContext => {
            this.isAuthenticated = !!(userContext.authTime);
            if (this.isAuthenticated) {
                this.roles = userContext.roles;
            }
        });
    }

    hasRole(roleName: string) {
        return this.roles.includes(roleName);
    }

    ngOnDestroy(): void {
        this.userContextSubscribtion.unsubscribe();
    }

    challengeOidc() {
        this.auth.challengeOidc(this.router.url);
    }

    logout() {
        this.auth.logout();
    }
}
