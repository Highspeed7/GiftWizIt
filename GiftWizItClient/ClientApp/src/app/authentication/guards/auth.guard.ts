import { Injectable } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { MsalService } from "@azure/msal-angular";
import * as authConfig from "../../configs/authConfig";
import { AuthService } from "../services/auth.service";
import { AccountsService } from "src/app/accounts.service";

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {
  constructor(
    private msal: MsalService,
    private authSvc: AuthService,
    private acntSvc: AccountsService
  ) { }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    let url: string = state.url;
    return this.checkLogin(url);
  }

  checkLogin(url: string) {
    // TODO: Move to auth service
    if (this.acntSvc.loggedInSrc.value != false) {
      return true;
    }
    // Store the redirect url
    this.authSvc.redirectUrl = url;
    this.authSvc.login();
  }
}
