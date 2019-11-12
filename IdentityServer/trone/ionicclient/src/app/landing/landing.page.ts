import { Component, OnInit } from '@angular/core';
import { OidcService } from '../oidc.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Platform } from '@ionic/angular';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.page.html',
  styleUrls: ['./landing.page.scss'],
})
export class LandingPage implements OnInit {

  user_claim_set:UserClaims;
  protectedData:any[] = [];
  
  constructor(private oidc:OidcService, private http:HttpClient, private platform:Platform) { 
    this.oidc.get_access_token().then(
      () => { 
        this.oidc.get_user_claims().then((user_claim_set:UserClaims) => {
          this.user_claim_set = new UserClaims();
          this.user_claim_set.email = user_claim_set.email;
          this.user_claim_set.birthdate = user_claim_set.birthdate;
          this.user_claim_set.gender = user_claim_set.gender;
          this.user_claim_set.favcolor = user_claim_set.favcolor;
        }
        ); 
      }
    );
  }

  ngOnInit() {}

  getProtectedData() {
    let isMobile : boolean = this.platform.is('cordova');
    let baseUrl : string = isMobile ? "10.0.2.2:5001" : "localhost:5001";
    let url : string = "http://" + baseUrl + "/api/product/secure";

    this.http.get(url, { 
        responseType: 'text', 
        headers: new HttpHeaders().set('Authorization',  "Bearer " + this.oidc.state.access_token) 
      })
      .subscribe(
        response => {
          this.protectedData.push(response);
        },
        err => alert('error : ' + JSON.stringify(err))
      );
  }

  refreshAccessToken(){
    this.oidc.get_new_access_token();
  }

  disconnect() {
  }
}

export class UserClaims{
  email:string;
  birthdate:string;
  gender:string;
  favcolor:string;
}