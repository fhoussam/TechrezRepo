import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { OidcService } from '../oidc.service';
import { HTTP } from '@ionic-native/http/ngx';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {

  // films: Observable<any>;
  // constructor(private httpClient: HttpClient) {
  //   this.httpClient.get('https://jsonplaceholder.typicode.com/todos').subscribe(function(data){ console.log(data); }); 
  // }

  constructor(
    private oidc:OidcService, 
    private http_desktop:HttpClient,
    private http_mobile: HTTP,
    private router:Router,
    ){
  }

  testNavigation(){
    this.router.navigateByUrl('/landing?first_name=houssam');
  }

  connect(){
    console.log('connecting user ...');
    this.oidc.authorize();
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
