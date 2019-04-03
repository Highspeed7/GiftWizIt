import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import { Observable, Subscription, BehaviorSubject } from 'rxjs';

import { RegisterModel } from './models/register';
import { LoginLocalModel } from './models/login';

@Injectable({
  providedIn: 'root'
})
export class AccountsService {
  public loggedInSrc: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(null);
  public logingedIn$ = this.loggedInSrc.asObservable();
}
