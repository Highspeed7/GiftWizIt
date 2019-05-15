import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { GiftList } from '../models/gift-list';
import { MsalService } from "@azure/msal-angular";
import { AuthService } from 'src/app/authentication/services/auth.service';
import * as authConfig from '../../configs/authConfig';
import { Router } from '@angular/router';
import { AccountsService } from 'src/app/accounts.service';

@Injectable({
  providedIn: 'root'
})
export class GiftListService {

  private apiUrl: string = "https://localhost:44327/api/GiftLists";

  constructor(private http: HttpClient,
    private authSvc: AuthService,
    private acntSvc: AccountsService,
    private msal: MsalService,
  ) { }

  public getLists() {
    var access = this.msal.getCachedTokenInternal(authConfig.config.b2cScopes);
    return this.http.get(`${this.apiUrl}`, { headers: { 'Authorization': `bearer ${access.token}` } })
      .map(res => res as GiftList[]);
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
}
