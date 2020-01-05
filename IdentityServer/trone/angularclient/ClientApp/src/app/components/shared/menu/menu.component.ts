import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit, OnDestroy {

    private userContextSubscribtion: Subscription;
    isAuthenticated = false;

    constructor(
        private auth: AuthService,
    ) {

    }

    ngOnInit() {
        this.userContextSubscribtion = this.auth.userContext.subscribe(userContext => {
            this.isAuthenticated = !!userContext;
        });
    }

    ngOnDestroy(): void {
        this.userContextSubscribtion.unsubscribe();
    }

    challengeOidc() {
        this.auth.challengeOidc();
    }

    logout() {
        this.auth.logout();
    }
}
