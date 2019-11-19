import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { OidcService } from '../oidc.service';
import { HTTP } from '@ionic-native/http/ngx';
import { Router } from '@angular/router';
import { InAppBrowser } from '@ionic-native/in-app-browser/ngx';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {

  constructor(
    private oidc:OidcService, 
    private http_desktop:HttpClient,
    private http_mobile: HTTP,
    private router:Router,
    private iab: InAppBrowser
    ){
  }

  testNavigation(){
    this.router.navigateByUrl('/landing?first_name=houssam');
  }

  connect(){
    let auth_url : string = this.oidc.authorize();
    const browser = this.iab.create(auth_url.toString(),'_blank','hidden=no,location=yes');
    browser.on('loadstop').subscribe(event => {
        var call_back_url = new URL(event.url);
        var code = call_back_url.searchParams.get("code");
        if(code != null){
            this.oidc.get_access_token(event.url).then(()=>{
              this.oidc.get_user_claims().then(()=>{
                this.router.navigateByUrl('/landing');
              });
            });
            browser.close();
        }
     });
  }

  testCors() {
    let isMobile : boolean = window.location.href.indexOf("8100") != -1;
    let baseUrl : string = isMobile ? "10.0.2.2:5001" : "localhost:5001";
    let url : string = "http://" + baseUrl + "/api/product/nonsecure";

    console.log('URL : ' + url);

    this.http_desktop.get('http://10.0.2.2:5000/nonsecure', 
      { responseType:'text' }
      )
      .subscribe(response => alert(JSON.stringify(response)), err => alert(JSON.stringify(err)));

    this.http_desktop.get(url, 
      { responseType:'text' }
      )
      .subscribe(response => alert(JSON.stringify(response)), err => alert(JSON.stringify(err)));
    
    this.http_mobile.get(url, null, null)
      .then(response => alert(JSON.stringify(response.data)))
      .catch(err => alert(JSON.stringify(err)));
  }
}
