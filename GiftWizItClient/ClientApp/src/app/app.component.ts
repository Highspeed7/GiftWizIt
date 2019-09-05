import { Component, OnInit, OnDestroy} from '@angular/core';
import { WindowRefService } from './window-ref.service';
import { AccountsService } from './accounts.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { MsalService, BroadcastService } from '@azure/msal-angular';
import { AuthService } from './authentication/services/auth.service';
import { NotificationsService } from './services/notifications.service';
import { Subscription } from 'rxjs';
import { AppInfo } from './models/appInfo';
import { environment } from 'src/environments/environment';
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
  public isNavbarCollapsed = true;
  public isAuthenticated: boolean = false;

  private appInfo: AppInfo = new AppInfo();
  private window: any;
  private isAuthenticatedSub: Subscription;
  private bcsLoginSuccessSub: Subscription;
  private bcsLoginFailSub: Subscription;
  // TODO: Move to a service

  // Injected msal service to handle redirect from auth server.
  constructor(
    private windowRef: WindowRefService,
    private notificationSvc: NotificationsService,
    private authSvc: AuthService,
    private bcs: BroadcastService,
    private msal: MsalService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.window = this.windowRef.nativeWindow;
    // Check for non-registered user experience
    if (this.authSvc.getNonRegisteredUserX() != null) {
      // Show the previously visited user card or link.
    }
  }

  public login() {
    this.authSvc.login();
  }

  public ngOnInit() {
    this.route.url.subscribe((url) => {
      console.log(url);
    });
    this.isAuthenticatedSub = this.authSvc.getAuthenticatedObs().subscribe((value) => {
      this.isAuthenticated = value;
      //if (this.isAuthenticated) {
      //  //if (this.route.url == environment.)
      //  this.router.navigate(["welcome"]);
      //} else {
      //  this.router.navigate([""]);
      //}
    });

    this.user = this.msal.getUser();
    if (this.user !== null) {
      this.displayName = this.user.name;
      this.authSvc.setAuthenticated(true);
    }

    this.notificationSvc.connect();

    this.bcsLoginSuccessSub = this.bcs.subscribe("msal:loginSuccess", (msg) => {
      this.user = this.msal.getUser();
      this.authSvc.setAuthenticated(true);
      this.authSvc.registerUser();
      this.displayName = this.user.name;

      //this.authSvc.registerUser().then((r) => {
      //  this.appInfo.userInfo['isRegistered'] = true;
      //});
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
    this.isAuthenticatedSub.unsubscribe();
  }
}
