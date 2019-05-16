import { Injectable } from '@angular/core';
import { GiftItem } from '../models/gift-item';
import { HttpClient } from '@angular/common/http';
import { MsalService } from '@azure/msal-angular';
import * as authConfig from "../../configs/authConfig";

@Injectable({
  providedIn: 'root'
})
export class WishItemService {
  private apiUrl: string = "https://localhost:44327/api/MoveItems"

  constructor(
    private http: HttpClient,
    private msal: MsalService
  ) { }

  public moveItems(itemsToMove: GiftItem[]) {
    var access = this.msal.getCachedTokenInternal(authConfig.config.b2cScopes);
    return this.http.post(`${this.apiUrl}`, itemsToMove, { headers: { 'Authorization': `bearer ${access.token}` } });
  }
}
