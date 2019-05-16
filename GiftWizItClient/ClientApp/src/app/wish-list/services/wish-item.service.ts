import { Injectable } from '@angular/core';
import { GiftItem } from '../models/gift-item';
import { HttpClient } from '@angular/common/http';
import { MsalService } from '@azure/msal-angular';
import * as authConfig from "../../configs/authConfig";
import { AuthService } from 'src/app/authentication/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class WishItemService {
  private apiUrl: string = "https://localhost:44327/api/MoveItems"
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
      retVal = this.http.post(`${this.apiUrl}`, itemsToMove, { headers: { 'Authorization': `bearer ${this.accessToken}` } }).toPromise();
      return retVal;
    } catch (e) {
      throw e;
    }
  }
}
