import { Injectable, OnDestroy } from '@angular/core';
import { WindowRefService } from 'src/app/window-ref.service';
import { Router } from '@angular/router';
import { AccountsService } from 'src/app/accounts.service';
import { MsalService, BroadcastService } from '@azure/msal-angular';
import { HttpClient } from '@angular/common/http';
import * as authConfig from '../../configs/authConfig';
import { AppInfo } from 'src/app/models/appInfo';
import { Subscription, Subject } from 'rxjs';
import { GWAppConstants } from 'src/app/constants/appConstants';
import { GuestInfo } from 'src/app/models/guestInfo';

@Injectable({
  providedIn: 'root'
})

export class AuthService implements OnDestroy {
  public redirectUrl = "/";
  private _isAuthenticatedSrc: Subject<boolean> = new Subject();
  private isAuthenticated$ = this._isAuthenticatedSrc.asObservable();
  private window: any;
  private isLoggedInSub: Subscription;
  private isLoggedOutSub: Subscription;
  constructor(
    private msal: MsalService,
    private bcs: BroadcastService,
    private http: HttpClient,
    private windowRef: WindowRefService,
    private acntSvc: AccountsService,
    private router: Router) {
    this.window = this.windowRef.nativeWindow;
  }
  public async login() {
    var promise = new Promise((resolve, reject) => {
      this.msal.loginPopup(authConfig.config.b2cScopes).then(async (res) => {
        await this.registerUser();
      });
    })
  }

  public setAuthenticated(value: boolean) {
    this._isAuthenticatedSrc.next(value);
  }

  public getAuthenticatedObs() {
    return this.isAuthenticated$;
  }

  public getToken(): Promise<string> {
    var cacheToken = this.msal.getCachedTokenInternal(authConfig.config.b2cScopes);
    if (cacheToken == null) {
      return this.msal.acquireTokenSilent(authConfig.config.b2cScopes)
        .then(token => {
          return Promise.resolve(token);
        }).catch(error => {
          this.msal.acquireTokenPopup(authConfig.config.b2cScopes)
            .then(async (token) => {
              await this.registerUser();
              return Promise.resolve(token);
            }).catch(innererror => {
              return Promise.resolve('');
            });
        });
    }
    else return Promise.resolve(cacheToken.token);
  }

  public async registerUser() {
    var access_token = null;
    await this.getToken().then((token) => {
      access_token = token;
    });
    return this.http.post("https://localhost:44327/api/Users", null, { headers: { 'Authorization': `bearer ${access_token}` } }).toPromise();
  }

  public getNonRegisteredUserX(): GuestInfo | null {
    // Check for guest info object in LS
    var guestInfoObj: GuestInfo = JSON.parse(this.window.localStorage.getItem(GWAppConstants.strGuestInfo));
    if (guestInfoObj != null) {
      return guestInfoObj;
    }
    return null;
  }

  public logout() {
    this.msal.logout();
    this.window.localStorage.removeItem("gw_app");
  }

  public ngOnDestroy() {
    this.isLoggedInSub.unsubscribe();
  }
}
