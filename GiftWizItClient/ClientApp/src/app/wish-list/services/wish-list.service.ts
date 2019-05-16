import { Injectable } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import * as authConfig from '../../configs/authConfig';
import { HttpClient } from '@angular/common/http';
import { WishList } from '../models/wish-list';
import { Observable } from 'rxjs';
import { GiftItem } from '../models/gift-item';

@Injectable({
  providedIn: 'root'
})
export class WishListService {

  private apiUrl: string = "https://localhost:44327/api/WishList";

  constructor(
    private http: HttpClient,
    private msal: MsalService
  ) { }

  public getLists(): Observable<WishList[]> {
    var access = this.msal.getCachedTokenInternal(authConfig.config.b2cScopes);

    return this.http.get(`${this.apiUrl}`, { headers: { 'Authorization': `bearer ${access.token}` } })
      .map(res => res as WishList[]);
  }
}
