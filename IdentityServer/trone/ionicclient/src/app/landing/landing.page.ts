import { Component, OnInit } from '@angular/core';
import { OidcService } from '../oidc.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';

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
          // this.user_claim_set.email = user_claim_set.email;
          this.user_claim_set.birthdate = user_claim_set.birthdate;
          this.user_claim_set.gender = user_claim_set.gender;
          this.user_claim_set.favcolor = user_claim_set.favcolor;
        }
        ); 
      }
    );
  }

  ngOnInit() {}

  getProtectedData(){

    const httpOptions = {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + this.oidc.state.access_token
      })
    };

    this.http.post('http://localhost:5001/api/product/secure', null, httpOptions)
    .subscribe(function(user){
      console.log('dfzfezfze');
    },
    err => {
      console.error(err);
    });
  }

  refreshAccessToken(){
    this.oidc.get_new_access_token();
  }

  disconnect() {
    return new Promise((resolve, reject) => {
        const formData = new FormData();
        formData.append('first_name', 'Houssam');
        formData.append('last_name', 'FERTAQ');

        this.http.post('http://localhost:5001/api/product/nonsecure', formData).subscribe(function(response){
          resolve(response);
        },
        err => {
          console.error(err);
          reject(err);
        });
    });
}
}

export class UserClaims{
  email:string;
  birthdate:string;
  gender:string;
  favcolor:string;
}