﻿class Oidc {

    constructor() {
        this.state = new State();
    }

    is_logged_in = function () {
        var access_token = localStorage.setItem('access_token');
        if (access_token) {
            return true;
        }
        return false;
    }

    generate_code_verifier = function (code_challenge) {
        var hash = KJUR.crypto.Util.hashString(code_challenge, 'sha256');
        return hextob64u(hash);
    }

    authorize = function () {
        var nonce = 'N' + Math.random() + '' + Date.now();
        var state = Date.now() + '' + Math.random() + Math.random();
        var code_verifier = 'C' + Math.random() + '' + Date.now() + '' + Date.now() + Math.random();
        var code_challenge = this.generate_code_verifier(code_verifier);

        localStorage.setItem('code_verifier', code_verifier);
        localStorage.setItem('local_state', state);

        var url = new URL(Wellknown.authorize_endpoint);
        url.searchParams.append('client_id', Wellknown.client_id);
        url.searchParams.append('redirect_uri', Wellknown.redirect_url);
        url.searchParams.append('response_type', 'code');
        url.searchParams.append('scope', 'email openid profile offline_access');
        url.searchParams.append('nonce', nonce);
        url.searchParams.append('state', state);
        url.searchParams.append('code_challenge', code_challenge);
        url.searchParams.append('code_challenge_method', 'S256');
        window.location.replace(url);
    }

    get_access_token = function () {
        return new Promise((resolve, reject) => {
            var local_state = localStorage.getItem('local_state');
            var code_verifier = localStorage.getItem('code_verifier');
            var url_string = window.location.href;
            var url = new URL(url_string);
            var code = url.searchParams.get("code");
            var server_state = url.searchParams.get("state");

            var data = "grant_type=authorization_code&client_id=" + Wellknown.client_id
                + ("&code_verifier=" + code_verifier + "&code="
                + code + "&redirect_uri=" + Wellknown.redirect_url);

            if (local_state === server_state) {
                var current_state = this.state;
                $.ajax({
                    url: Wellknown.token_endpoint,
                    type: 'post',
                    data: data,
                    success: function (resp) {
                        current_state.access_token = resp.access_token;
                        resolve(resp.access_token);
                    },
                    error: function (err) {
                        console.log(err);
                        reject(err);
                    }
                });
            }
            else
                reject('invalid state');
        });
    }

    get_user_claims = function () {
        return new Promise((resolve, reject) => {
            $.ajax({
                url: Wellknown.userinfo_endpoint,
                type: 'post',
                headers: {
                    "Authorization": "Bearer " + access_token
                },
                success: function (userid) {
                    this.state.UserClaimSet.email = userid.email;
                    resolve(this.state.UserClaimSet);
                },
                error: function (err) {
                    console.log(err);
                    reject(err);
                }
            });
        });
    }
}

class State {

    constructor() {
        this.UserClaimSet = new UserClaimSet();
    }

    get access_token() {
        return localStorage.getItem('access_token');
    }

    set access_token(value) {
        localStorage.setItem('access_token', value);
    }

    get user_claim_set() {
        return JSON.parse(localStorage.getItem('user_claim_set'));
    }

    set user_claim_set(value) {
        localStorage.setItem('user_claim_set', value);
    }
}

class UserClaimSet {
    get email() {
        return this.get_claim('email');
    }

    set email(value) {
        this.set_claim('email', value);
    }

    get_claim = function (key) {
        var user_claim_set = JSON.parse(localStorage.getItem('user_claim_set'));
        return claims_object[key];
    }

    set_claim = function (key, value) {
        var user_claim_set = JSON.parse(localStorage.getItem('user_claim_set'));
        claims_object[key] = value;
        localStorage.setItem('user_claim_set', JSON.stringify(user_claim_set));
    }
}

class Wellknown {
    static client_id = 'jsclient';
    static token_endpoint = 'http://localhost:5000/connect/token';
    static redirect_url = 'http://localhost:5003/callback.html';
    static userinfo_endpoint = 'http://localhost:5000/connect/userinfo';
    static authorize_endpoint = 'http://localhost:5000/connect/authorize';
}