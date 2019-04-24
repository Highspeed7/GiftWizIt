import { Component, OnInit} from '@angular/core';
import { WindowRefService } from './window-ref.service';
import { AccountsService } from './accounts.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { map } from 'rxjs/operators';
import { URLSearchParams } from '@angular/http';
import { MsalService } from '@azure/msal-angular';
import { AuthService } from './authentication/services/auth.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public title = 'app';
  public displayName: string = "Guest";
  public isLoggedIn: boolean = false;
  private window: any;

  // Injected msal service to handle redirect from auth server.
  constructor(
    private windowRef: WindowRefService,
    private acntSvc: AccountsService,
    private authSvc: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private msal: MsalService) {
    this.window = this.windowRef.nativeWindow;
    
    // TODO: remove in favor of reading cachedToken to determine logged in state.
    this.acntSvc.loggedIn$.subscribe((l: boolean) => {
      this.isLoggedIn = l;
      this.setDisplayName();
    });
  }

  public setDisplayName() {
    if (this.window.localStorage.getItem("gw_app")) {
      var userInfo = JSON.parse(this.window.localStorage.getItem("gw_app"));
      this.displayName = userInfo.userInfo.name;
    }
  }

  public ngOnInit() {
    var url = this.route.url.subscribe((v) => {
      console.log(v);
    });
    if(this.route.url)
    if (this.acntSvc.isLoggedIn()) {
      this.isLoggedIn = true;
      this.setDisplayName();
    }
  }

  public onLogout() {
    this.authSvc.logout();
  }
}
