import { Injectable } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import * as authConfig from '../../configs/authConfig';
import { HttpClient } from '@angular/common/http';
import { WishList } from '../models/wish-list';
import { Observable } from 'rxjs';
import { GiftItem } from '../models/gift-item';
import { AuthService } from 'src/app/authentication/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class WishListService {

  private apiUrl: string = "https://localhost:44327/api/WishList";
  private access = null;

  constructor(
    private http: HttpClient,
    private msal: MsalService,
    private authSvc: AuthService
  ) { }

  public async getLists() {
    await this.authSvc.getToken().then((token) => {
      this.access = token;
    })

    return this.http.get(`${this.apiUrl}`, { headers: { 'Authorization': `bearer ${this.access}` } })
      .map(res => res as WishList[]).toPromise();
  }
}
