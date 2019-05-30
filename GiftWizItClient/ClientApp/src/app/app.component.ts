import { Component, OnInit, OnDestroy} from '@angular/core';
import { WindowRefService } from './window-ref.service';
import { AccountsService } from './accounts.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
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
  private bcsLoginSuccessSub: Subscription;
  private bcsLoginFailSub: Subscription;
  // TODO: Move to a service
  private isAuthenticated: boolean = false;

  // Injected msal service to handle redirect from auth server.
  constructor(
    private windowRef: WindowRefService,
    private authSvc: AuthService,
    private bcs: BroadcastService,
    private msal: MsalService) {
    this.window = this.windowRef.nativeWindow;
  }

  public login() {
    this.authSvc.login().then(() => { });
  }
  public ngOnInit() {
    this.user = this.msal.getUser();
    if (this.user !== null) {
      this.displayName = this.user.name;
      this.isAuthenticated = true;
    }
    this.bcsLoginSuccessSub = this.bcs.subscribe("msal:loginSuccess", (msg) => {
      this.user = this.msal.getUser();
      this.isAuthenticated = true;
      this.displayName = this.user.name;

      this.authSvc.registerUser().then((r) => {
        this.appInfo.userInfo['isRegistered'] = true;
      });
    });

    this.bcsLoginFailSub = this.bcs.subscribe("msal:loginFailure", (msg) => {
      console.log(msg);
      switch (msg.errorDesc) {
        case "interaction_required":
          this.authSvc.getToken();
          break;
      }
    });
  }

  public onLogout() {
    this.authSvc.logout();
  }

  public ngOnDestroy() {
    this.bcsLoginSuccessSub.unsubscribe();
    this.bcsLoginFailSub.unsubscribe();
  }
}
