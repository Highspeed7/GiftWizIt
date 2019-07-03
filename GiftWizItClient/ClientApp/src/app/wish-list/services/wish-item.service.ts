import { Injectable } from '@angular/core';
import { GiftItem } from '../models/gift-item';
import { HttpClient } from '@angular/common/http';
import { MsalService } from '@azure/msal-angular';
import * as authConfig from "../../configs/authConfig";
import { AuthService } from 'src/app/authentication/services/auth.service';
import * as env from '../../../environments/environment';
import { Item } from '../models/item';

@Injectable({
  providedIn: 'root'
})
export class WishItemService {
  private apiUrl: string = env.environment.apiUrl;
  private accessToken = null;

  constructor(
    private http: HttpClient,
    private authSvc: AuthService
  ) { }

  public async moveItems(itemsToMove: GiftItem[]) {
    await this.authSvc.getToken().then((token) => {
      this.accessToken = token;
    });
    var retVal;
    try {
      retVal = this.http.post(`${this.apiUrl}/MoveItems`, itemsToMove, { headers: { 'Authorization': `bearer ${this.accessToken}` } }).toPromise();
      return retVal;
    } catch (e) {
      throw e;
    }
  }

  public async deleteItems(itemsToDelete: Item[]) {
    await this.authSvc.getToken().then((token) => {
      this.accessToken = token;
    });

    return this.http.post(`${this.apiUrl}/WishList/ItemDelete`, itemsToDelete, { headers: { 'Authorization': `bearer ${this.accessToken}` } }).toPromise();
  }
}
