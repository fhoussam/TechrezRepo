import { Component, OnInit } from '@angular/core';
import { OidcService } from '../oidc.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.page.html',
  styleUrls: ['./landing.page.scss'],
})
export class LandingPage implements OnInit {

  user_claim_set:UserClaims;
  protectedData:Array<any>;
  constructor(private oidc:OidcService, private http:HttpClient) { 
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
    let isMobile : boolean = window.location.href.indexOf("8100") != -1;
    let baseUrl : string = isMobile ? "10.0.2.2:5001" : "localhost:5001";
    let url : string = "http://" + baseUrl + "/api/product/secure";

    this.http.get(url, { 
        responseType: 'text', 
        headers: new HttpHeaders().set('Authorization',  "Bearer " + this.oidc.state.access_token) 
      })
      .subscribe(
        response => alert(JSON.stringify('success : ' + response)), 
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