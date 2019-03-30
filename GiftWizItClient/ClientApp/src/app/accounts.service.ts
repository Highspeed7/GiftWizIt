import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import { Observable, Subject, Subscription } from 'rxjs';

import { RegisterModel } from './models/register';
import { LoginLocalModel } from './models/login';

@Injectable({
    providedIn: 'root'
})
export class AccountsService {
    public loggedInSrc: Subject<boolean> = new Subject<boolean>();
    public logingedIn$ = this.loggedInSrc.asObservable();

    private apiUrl: string = "api/account";
    private authUrl: string = "/Token";

  constructor(private http: Http) {  }

    public registerUser(email: string, password: string, confPass: string): Observable<any> {
        var registerObj: RegisterModel = {
            email: email,
            password: password,
            confirmPassword: confPass
        };

        return this.http.post(`${this.apiUrl}/register`, registerObj);
    }

    public loginUserLocal(email: string, password: string): Observable<any> {
        var loginObj: LoginLocalModel = {
            username: email,
            password: password,
            grant_type: "password"
        };

        var headers = new Headers();
        headers.append('Content-Type', 'application/x-www-form-urlencoded');

        var options = new RequestOptions({ headers: headers });

        let urlParams = new URLSearchParams();
        urlParams.set('grant_type', loginObj.grant_type);
        urlParams.set('username', loginObj.username);
        urlParams.set('password', loginObj.password);

        let body = urlParams.toString();

        return this.http.post(this.authUrl, body, options);
    }

    public logout() {
        // Get access token
        let access_token = localStorage.getItem("access_token");
        let headers = new Headers();
        headers.append("Authorization", `bearer ${access_token}`);
        let options = new RequestOptions({ headers: headers });

        return this.http.post(`${this.apiUrl}/logout`, null, options);
    }

    public getExternalUrl(): Observable<any> {
        // Obtain external urls
        return this.http.get(`${this.apiUrl}/ExternalLogins?returnUrl=/login&generateState=true`);
    }

    public doSocialLogin(url: string) {
        return this.http.get(`${url}`);
    }

    public isUserRegistered(access_token: string) {
        return this.http.get(`${this.apiUrl}/UserInfo`, new RequestOptions({ headers: new Headers({ 'Authorization': `Bearer ${access_token}` }) }));
    }

    public signupExternalUser(access_token: string) {
        var headers = new Headers({
            'content-type': 'application/json',
            'Authorization': `Bearer ${access_token}`
        });

        return this.http.post(`${this.apiUrl}/RegisterExternal`, null, new RequestOptions({ headers: headers }));
    }
}
