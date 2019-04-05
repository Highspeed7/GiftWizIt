import { Injectable } from '@angular/core';
import { WindowRefService } from 'src/app/window-ref.service';
import { Router } from '@angular/router';
import { AccountsService } from 'src/app/accounts.service';
import { MsalService } from '@azure/msal-angular';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private window: any;
  private authConfig = {
    b2cScopes: ["http://giftwizit.onmicrosoft.com/api/read"]
  };

  constructor(
    private msal: MsalService,
    private http: HttpClient,
    private windowRef: WindowRefService,
    private acntSvc: AccountsService,
    private router: Router) {
    this.window = this.windowRef.nativeWindow;
  }

  public login() {
    this.msal.loginPopup(this.authConfig.b2cScopes).then((r) =>
    {
      this.msal.acquireTokenSilent(this.authConfig.b2cScopes).then((res) => {
        //this.http.get("https://localhost:44327/api/values", { headers: { 'Authorization': `bearer ${res}` } }).subscribe((response) => {
        //});
      });
    });
  }
}
