import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { AccountsService } from '../accounts.service';
import { WindowRefService } from '../window-ref.service';
import { Router } from '@angular/router';
import { AuthService } from '../authentication/services/auth.service';

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
    private authSvc: AuthService,
    private windowRef: WindowRefService,
    private router: Router) {
    this.window = this.windowRef.nativeWindow;
  }

  ngOnInit() {
    this.authSvc.login();
  }
}
