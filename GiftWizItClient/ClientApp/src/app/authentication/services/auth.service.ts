import { Injectable, OnDestroy } from '@angular/core';
import { WindowRefService } from 'src/app/window-ref.service';
import { Router } from '@angular/router';
import { AccountsService } from 'src/app/accounts.service';
import { MsalService, BroadcastService } from '@azure/msal-angular';
import { HttpClient } from '@angular/common/http';
import * as authConfig from '../../configs/authConfig';
import { AppInfo } from 'src/app/models/appInfo';
import { Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AuthService implements OnDestroy {
  public redirectUrl = "/";
  private window: any;
  private appInfo: AppInfo = new AppInfo();
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

    this.isLoggedInSub = this.bcs.subscribe("msal:loginSuccess", (msg) => {
      this.acntSvc.loggedInSrc.next(true);
      // Store the user info
      var user = this.msal.getUser();
      // TODO: replace with modeled property.
      this.appInfo.userInfo = user;
      this.window.localStorage.setItem("gw_app", JSON.stringify(this.appInfo));
      this.registerUser().then((r) => {
        if (r > 0) {
          this.appInfo.userInfo['isRegistered'] = true;
        } else {
          this.appInfo.userInfo['isRegistered'] = false;
        }
      });
    });
  }
  public async login() {
    var promise = new Promise((resolve, reject) => {
      this.msal.loginPopup(authConfig.config.b2cScopes).then(() => {})
    })
  }

  public getToken(): Promise<string> {
    var cacheToken = this.msal.getCachedTokenInternal(authConfig.config.b2cScopes);
    if (cacheToken == null) {
      return this.msal.acquireTokenSilent(authConfig.config.b2cScopes)
        .then(token => {
          return Promise.resolve(token);
        }).catch(error => {
          this.msal.acquireTokenPopup(authConfig.config.b2cScopes)
            .then(token => {
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

  //public login() {
  //  var cacheRes = this.msal.getCachedTokenInternal(authConfig.config.b2cScopes);
  //  if (!cacheRes) {
  //    this.msal.loginPopup(authConfig.config.b2cScopes).then((r) => {
  //      this.msal.acquireTokenSilent(authConfig.config.b2cScopes).then((res) => {
  //        console.log("token response: " + res);
  //        var user: any = this.msal.getUser();
  //        // Register the user in database.
  //        // TODO: Move to server
  //        this.http.post("https://localhost:44327/api/Users", { userId: `${user.idToken.oid}` }, { headers: { 'Authorization': `bearer ${res}` } })
  //          .subscribe((response) => {
  //            this.acntSvc.loggedInSrc.next(true);
  //            this.router.navigate([this.redirectUrl]);
  //          });
  //      });
  //    });
  //  } else {
  //    this.acntSvc.loggedInSrc.next(true);
  //    this.router.navigate([this.redirectUrl]);
  //  }
  //}

  public logout() {
    this.msal.logout();
    this.window.localStorage.removeItem("gw_app");
  }

  public ngOnDestroy() {
    this.isLoggedInSub.unsubscribe();
  }
}
