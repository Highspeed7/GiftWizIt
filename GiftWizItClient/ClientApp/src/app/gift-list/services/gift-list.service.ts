import { Injectable, OnDestroy } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { GiftList } from '../models/gift-list';
import { MsalService, BroadcastService } from "@azure/msal-angular";
import { AuthService } from 'src/app/authentication/services/auth.service';
import * as authConfig from '../../configs/authConfig';
import { Router } from '@angular/router';
import { AccountsService } from 'src/app/accounts.service';
import { Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GiftListService implements OnDestroy {

  private apiUrl: string = "https://localhost:44327/api/GiftLists";
  private access = null;
  private subscription: Subscription

  constructor(private http: HttpClient,
    private bcs: BroadcastService,
    private authSvc: AuthService,
    private msal: MsalService,
  ) {
    this.subscription = this.bcs.subscribe("msal:acquireTokenSuccess", (payload) => {

    });
  }

  public async getLists() {
    await this.authSvc.getToken().then((token) => {
      this.access = token;
    });
    return this.http.get(`${this.apiUrl}`, { headers: { 'Authorization': `bearer ${this.access}` } })
      .map(res => res as GiftList[]).toPromise();
  }

  public createList(body: GiftList) {
    var access = this.msal.getCachedTokenInternal(authConfig.config.b2cScopes);

    return this.http.post(`${this.apiUrl}`, body, { headers: { 'Authorization': `bearer ${access.token}` } });
  }

  public deleteList(body: GiftList) {
    var access = this.msal.getCachedTokenInternal(authConfig.config.b2cScopes);

    const options = {
      headers: new HttpHeaders({
        'Authorization': `bearer ${access.token}`,
        'Content-Type': 'application/json'
      }),
      body: body
    }

    return this.http.delete(`${this.apiUrl}`, options);
  }

  ngOnDestroy() {
    this.bcs.getMSALSubject().next(1);
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
