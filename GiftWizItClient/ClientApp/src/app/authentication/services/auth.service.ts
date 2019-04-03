import { Injectable } from '@angular/core';
import { WindowRefService } from 'src/app/window-ref.service';
import { Router } from '@angular/router';
import { AccountsService } from 'src/app/accounts.service';
import { HttpClient } from '@angular/common/http';
import * as Msal from 'msal';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private window: any;

  constructor(
    private http: HttpClient,
    private windowRef: WindowRefService,
    private acntSvc: AccountsService,
    private router: Router) {
    this.window = this.windowRef.nativeWindow;
  }

  public login() {
    //Msal
  }
}
