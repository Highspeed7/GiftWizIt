import { Component, OnInit, OnDestroy} from '@angular/core';
import { WindowRefService } from './window-ref.service';
import { AccountsService } from './accounts.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { map } from 'rxjs/operators';
import { URLSearchParams } from '@angular/http';
import { MsalService, BroadcastService } from '@azure/msal-angular';
import { AuthService } from './authentication/services/auth.service';
import { Subscription } from 'rxjs';
import { AppInfo } from './models/appInfo';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  public title = 'app';
  public displayName: string = "Guest";
  public isLoggedIn: boolean = false;
  public user: any;
  private appInfo: AppInfo = new AppInfo();
  private window: any;
  private bcsSub: Subscription
  private isAuthenticated: boolean = false;

  // Injected msal service to handle redirect from auth server.
  constructor(
    private windowRef: WindowRefService,
    private acntSvc: AccountsService,
    private authSvc: AuthService,
    private bcs: BroadcastService,
    private router: Router,
    private route: ActivatedRoute,
    private msal: MsalService) {
    this.window = this.windowRef.nativeWindow;
    
    // TODO: remove in favor of reading cachedToken to determine logged in state.
    //this.acntSvc.loggedIn$.subscribe((l: boolean) => {
    //  this.isLoggedIn = l;
    //  //this.setDisplayName();
    //});
  }

  //public setDisplayName() {
  //  if (this.window.localStorage.getItem("gw_app")) {
  //    var userInfo = JSON.parse(this.window.localStorage.getItem("gw_app"));
  //    this.displayName = userInfo.userInfo.name;
  //  }
  //}
  public login() {
    this.authSvc.login().then(() => { });
  }
  public ngOnInit() {
    this.user = this.msal.getUser();
    if (this.user !== null) {
      this.displayName = this.user.name;
      this.isAuthenticated = true;
    }
    this.bcsSub = this.bcs.subscribe("msal:loginSuccess", (msg) => {
      this.user = this.msal.getUser();
      this.isAuthenticated = true;
      this.displayName = this.user.name;

      this.authSvc.registerUser().then((r) => {
        this.appInfo.userInfo['isRegistered'] = true;
      });
    });
    //this.authSvc.getToken().then((token) => {
    //  if (token != null) {
    //    this.isAuthenticated = true;
    //  } else {
    //    this.isAuthenticated = false;
    //  }
    //});
    //this.user = this.msal.getUser();
    //this.displayName = this.user.name;
    //var url = this.route.url.subscribe((v) => {
    //  console.log(v);
    //});
    //if(this.route.url)
    //if (this.acntSvc.isLoggedIn()) {
    //  this.isLoggedIn = true;
    //  this.setDisplayName();
    //}
  }

  public onLogout() {
    this.authSvc.logout();
  }

  public ngOnDestroy() {
    this.bcsSub.unsubscribe();
  }
}
