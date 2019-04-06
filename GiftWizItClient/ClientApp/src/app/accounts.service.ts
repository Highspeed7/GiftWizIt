import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { MsalService } from '@azure/msal-angular';
import * as authConfig from './configs/authConfig';

@Injectable({
  providedIn: 'root'
})
export class AccountsService {
  public loggedInSrc: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(null);
  public logingedIn$ = this.loggedInSrc.asObservable();

  constructor(
    private msal: MsalService) { }

  public isLoggedIn() {
    var tokenCache = this.msal.getCachedTokenInternal(authConfig.config.b2cScopes);
    return (!!tokenCache);
  }
}
