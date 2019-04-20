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
      this.router.navigate(["/"]);
    });
  }

  public login() {
    var cacheRes = this.msal.getCachedTokenInternal(authConfig.config.b2cScopes);
    if (!cacheRes) {
      this.msal.loginPopup(authConfig.config.b2cScopes).then((r) => {
        this.msal.acquireTokenSilent(authConfig.config.b2cScopes).then((res) => {
          console.log("token response: " + res);
          var user: any = this.msal.getUser();
          // Register the user in database.
          this.http.post("https://localhost:44327/api/Users", { userId: `${user.idToken.oid}` }, { headers: { 'Authorization': `bearer ${res}` } })
            .subscribe((response) => {
              console.log(response);
            });
        });
      });
    } else {
      // We're already logged in so redirect back to main page.
      this.router.navigate(["/"]);
    }
  }

  public logout() {
    this.msal.logout();
    this.window.localStorage.removeItem("gw_app");
  }

  public ngOnDestroy() {
    this.isLoggedInSub.unsubscribe();
  }
}
