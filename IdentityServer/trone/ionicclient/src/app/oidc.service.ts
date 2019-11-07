import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { sha256 } from 'js-sha256';
import { Base64 } from 'js-base64';
import { KJUR, hextob64u } from 'jsrsasign';
// import { hex64 } from 'hex64';
import * as hex64 from 'hex64';

@Injectable({
  providedIn: 'root'
})
export class OidcService {

  atltThreshold : number = 3580;
  checkAtltFrequency : number = 2000;
  state : State;
  interval : any;

  constructor(private http: HttpClient) {
      this.state = new State();
      this.interval = null;

      //because when we construct oidc on page load, we lose the instance which contains the interval property state
      //thus, this temporary and we can get rid off it once we get to benefit from pre built angular DI system
      if (this.state.access_token != null) {
          this.setAccessTokenInrerval();
      }
  }

  is_logged_in():boolean {
      var access_token = localStorage.getItem('access_token');
      if (access_token) {
          return true;
      }
      return false;
  }

  parseJwt(token):any {
      var base64Url = token.split('.')[1];
      var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
      }).join(''));

      return JSON.parse(jsonPayload);
  };

    generate_code_verifier (code_challenge) {
        var hash = KJUR.crypto.Util.hashString(code_challenge, 'sha256');
        var r = hextob64u(hash);
        return r;
    }

  authorize() {
      let nonce = 'N' + Math.random() + '' + Date.now();
      let state = Date.now() + '' + Math.random() + Math.random();
      let code_verifier = 'C' + Math.random() + '' + Date.now() + '' + Date.now() + Math.random();
      let code_challenge = this.generate_code_verifier(code_verifier);

      localStorage.setItem('code_verifier', code_verifier);
      localStorage.setItem('local_state', state);

      let url = new URL(Wellknown.authorize_endpoint);
      url.searchParams.append('client_id', Wellknown.client_id);
      url.searchParams.append('redirect_uri', Wellknown.redirect_url);
      url.searchParams.append('response_type', 'code');
      url.searchParams.append('scope', Wellknown.scopes);
      url.searchParams.append('nonce', nonce);
      url.searchParams.append('state', state);
      url.searchParams.append('code_challenge', code_challenge);
      url.searchParams.append('code_challenge_method', 'S256');
      window.location.replace(url.toString());
  }

  get_new_access_token() {
      return new Promise((resolve, reject) => {

          const formData = new FormData();
          formData.append('grant_type', 'refresh_token');
          formData.append('client_id', Wellknown.client_id);
          formData.append('refresh_token', this.state.refresh_token);

          //transpiler got wrecked in here :/
          var local_oidc = this;

          this.http.post(Wellknown.token_endpoint, formData)
          .subscribe(function(newtokens:any){
            this.state = new State();
            this.state.access_token = newtokens.access_token;
            this.state.id_token = newtokens.id_token;
            local_oidc.setAccessTokenInrerval();
            resolve(newtokens);
          },
          err => {
            console.error(err);
            reject(err);
          });
      });
  }

  setAccessTokenInrerval() {
    if (this.interval) clearInterval(this.interval);
    this.interval = setInterval(() => {
        var tokenLifeTime = this.access_token_life_time;
        if (tokenLifeTime < this.atltThreshold) {
            console.log('Access token is about to expire, getting a fresh token ...');
            this.get_new_access_token();
        }
        else {
            var message = 'Access token expires in : ' + this.access_token_life_time + ' seconds.';
            console.log(message);
        }
    }, this.checkAtltFrequency);
  }

  get_access_token() {
      return new Promise((resolve, reject) => {
          var local_state = localStorage.getItem('local_state');
          var code_verifier = localStorage.getItem('code_verifier');
          var url_string = window.location.href;
          var url = new URL(url_string);
          var code = url.searchParams.get("code");
          var server_state = url.searchParams.get("state");

          const formData = new FormData();
          formData.append('grant_type', 'authorization_code');
          formData.append('client_id', Wellknown.client_id);
          formData.append('code_verifier', code_verifier);
          formData.append('code', code);
          formData.append('redirect_uri', Wellknown.redirect_url);

          //transpiler got wrecked in here :/
          var local_oidc = this;

          if (local_state === server_state) {
              this.http.post(Wellknown.token_endpoint, formData)
              .subscribe(function(newtokens:any){
                this.state = new State();
                this.state.access_token = newtokens.access_token;
                this.state.id_token = newtokens.id_token;
                this.state.refresh_token = newtokens.refresh_token;
                local_oidc.setAccessTokenInrerval();
                resolve(newtokens);
              },
              err => {
                console.error(err);
                reject(err);
              });
          }
          else
              reject('invalid state');
      });
  }

  get_user_claims = function () {
      var current_state = this.state;
      return new Promise((resolve, reject) => {

        const httpOptions = {
            headers: new HttpHeaders({
                "Authorization": "Bearer " + this.state.access_token
            })
        };

        this.http.post(Wellknown.userinfo_endpoint, null, httpOptions).subscribe(function(user:any){
            current_state.userClaimSet = new UserClaimSet();
            current_state.userClaimSet.email = user.email;
            current_state.userClaimSet.birthdate = user.birthdate;
            current_state.userClaimSet.favcolor = user.favcolor;
            current_state.userClaimSet.gender = user.gender;
            resolve(current_state.userClaimSet);
        },
        err => {
            console.error(err);
            reject(err);
        });
      });
  }

  get access_token_life_time()
  {
      return (new Date(this.parseJwt(this.state.access_token).exp * 1000).getTime() - new Date().getTime()) / 1000;
  }
}

export class Wellknown {
  static client_id : string = 'ionicclient';
  static token_endpoint : string = 'http://localhost:5000/connect/token';
  // static redirect_url : string = 'http://localhost:8100/assets/callback.html';
  static redirect_url : string = 'http://localhost:8100/landing';
  static userinfo_endpoint : string = 'http://localhost:5000/connect/userinfo';
  static authorize_endpoint : string = 'http://localhost:5000/connect/authorize';
  static scopes : string = 'openid profile api1 email complementary_profile offline_access';
}

export class State {

  userClaimSet:UserClaimSet;

  constructor() {
      this.userClaimSet = new UserClaimSet();
  }

  get access_token() {
      return localStorage.getItem('access_token');
  }

  set access_token(value) {
      localStorage.setItem('access_token', value);
  }

  get refresh_token() {
      return localStorage.getItem('refresh_token');
  }

  set refresh_token(value) {
      localStorage.setItem('refresh_token', value);
  }

  get id_token() {
      return localStorage.getItem('id_token');
  }

  set id_token(value) {
      localStorage.setItem('id_token', value);
  }

  get user_claim_set() {
      return JSON.parse(localStorage.getItem('user_claim_set'));
  }

  set user_claim_set(value) {
      localStorage.setItem('user_claim_set', value);
  }
}

export class UserClaimSet {
//   get email() {
//       return this.get_claim('email');
//   }

//   set email(value) {
//       this.set_claim('email', value);
//   }

  get_claim = function (key) {
      var user_claim_set = JSON.parse(localStorage.getItem('user_claim_set'));
      return user_claim_set[key];
  }

  set_claim = function (key, value) {
      var user_claim_set = JSON.parse(localStorage.getItem('user_claim_set'));
      if (!user_claim_set) user_claim_set = {};
      user_claim_set[key] = value;
      localStorage.setItem('user_claim_set', JSON.stringify(user_claim_set));
  }
}