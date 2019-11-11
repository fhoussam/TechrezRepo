import { Component } from '@angular/core';

import { Platform } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { Deeplinks } from '@ionic-native/deeplinks/ngx';
import { OidcService } from './oidc.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss']
})
export class AppComponent {
  constructor(
    private platform: Platform,
    private splashScreen: SplashScreen,
    private statusBar: StatusBar,
    private deeplinks: Deeplinks,
    private oidc:OidcService,
    private router:Router,
  ) {
    this.initializeApp();
  }

  initializeApp() {
    this.platform.ready().then(() => {
      this.statusBar.styleDefault();
      this.splashScreen.hide();

      this.deeplinks.route({
        '/': {},
      }).subscribe(match => {
        // this.oidc.get_access_token(match.$link.url).then((tokens) => { });
        //navigate to landing page and send the response URL to it
        this.router.navigateByUrl('/landing?' + match.$link.queryString);
      }, nomatch => {
        alert('Got a deeplink that didn\'t match' + JSON.stringify(nomatch));
      });

    });
  }
}
