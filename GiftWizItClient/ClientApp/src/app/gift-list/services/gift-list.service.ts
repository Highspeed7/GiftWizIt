import { Injectable, OnDestroy } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { GiftList, GiftItemQuery } from '../models/gift-list';
import { MsalService, BroadcastService } from "@azure/msal-angular";
import { AuthService } from 'src/app/authentication/services/auth.service';
import * as authConfig from '../../configs/authConfig';
import { Router } from '@angular/router';
import { AccountsService } from 'src/app/accounts.service';
import { Subscription } from 'rxjs';
import { GiftItem } from 'src/app/wish-list/models/gift-item';
import { ListShare } from 'src/app/list-action/share-gift-list/models/list-share';
import * as env from '../../../environments/environment';
import { EditListModel } from 'src/app/list-action/edit-gift-list/models/edit-contact';

@Injectable({
  providedIn: 'root'
})
export class GiftListService implements OnDestroy {
  private apiUrl: string = env.environment.apiUrl;
  private access = null;
  private subscription: Subscription
  private accessToken: string;

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
    return this.http.get(`${this.apiUrl}/GiftLists`, { headers: { 'Authorization': `bearer ${this.access}` } })
      .map(res => res as GiftList[]).toPromise();
  }

  public async createList(body: GiftList) {
    await this.authSvc.getToken().then(token => this.access = token);
    return this.http.post(`${this.apiUrl}/GiftLists`, body, { headers: { 'Authorization': `bearer ${this.access}` } }).toPromise();
  }

  public deleteList(body: GiftList) {
    this.access = this.authSvc.getToken().then(token => this.access = token);

    const options = {
      headers: new HttpHeaders({
        'Authorization': `bearer ${this.access.token}`,
        'Content-Type': 'application/json'
      }),
      body: body
    }

    return this.http.delete(`${this.apiUrl}/GiftLists`, options);
  }

  public async deleteItems(body: GiftItemQuery[]) {
    await this.authSvc.getToken().then((token) => {
      this.accessToken = token;
    });
    
    var retVal;
    try {
      retVal = this.http.post(`${this.apiUrl}/GiftListItems`, body, { headers: { 'Authorization': `bearer ${this.accessToken}` } }).toPromise();
    } catch (e) {
      throw e;
    }

    return retVal;
  }

  public async getGiftItems(glist_id: number) {
    await this.authSvc.getToken().then(token => this.access = token);
    return this.http.get(`${this.apiUrl}/GiftListItems?gift_list_id=${glist_id}`, { headers: { 'Authorization': `bearer ${this.access}` } }).toPromise();
  }

  public async moveItems(itemsToMove: GiftItem[]) {
    await this.authSvc.getToken().then((token) => {
      this.accessToken = token;
    });
    var retVal;
    try {
      retVal = this.http.post(`${this.apiUrl}/MoveGiftItem`, itemsToMove, { headers: { 'Authorization': `bearer ${this.accessToken}` } }).toPromise();
      return retVal;
    } catch (e) {
      throw e;
    }
  }

  public async shareList(listToShare: ListShare) {
    await this.authSvc.getToken().then((token) => {
      this.accessToken = token;
    });
    return this.http.post(`${this.apiUrl}/ShareGiftList`, listToShare, { headers: { 'Authorization': `bearer ${this.accessToken}` } }).toPromise();
  }

  public async updateList(updatedList: EditListModel) {
    await this.authSvc.getToken().then((token) => {
      this.accessToken = token;
    });
    return this.http.post(`${this.apiUrl}/GiftLists/Update`, updatedList, { headers: { 'Authorization': `bearer ${this.accessToken}` } }).toPromise();
  }

  ngOnDestroy() {
    this.bcs.getMSALSubject().next(1);
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
