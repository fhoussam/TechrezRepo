import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { OidcService } from '../oidc.service';

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

  constructor(private oidc:OidcService){
  }

  connect(){
    console.log('connecting user ...');
    this.oidc.authorize();
  }
}
