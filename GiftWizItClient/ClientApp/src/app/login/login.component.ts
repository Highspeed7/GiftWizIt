import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { AccountsService } from '../accounts.service';
import { WindowRefService } from '../window-ref.service';
import { Router } from '@angular/router';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public loginForm: FormGroup;
  public socialLogins = [];
  private window: any;
  private socialAuthUrl: string = '';

  constructor(
    private msal: MsalService,
    private windowRef: WindowRefService,
    private router: Router) {
    this.window = this.windowRef.nativeWindow;
  }

  ngOnInit() {
    this.msal.loginPopup(["https://giftwizit.onmicrosoft.com/api/user_impersonation"]).then((r) => {
      this.window.localStorage.setItem("gw_access_token", r);
    });
  }
}
