import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from 'src/app/authentication/services/auth.service';
import * as env from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ShareGiftListService {
  public token: string;
  private apiUrl = env.environment.apiUrl;

  constructor(private http: HttpClient, private authSvc: AuthService) { }

  public async getGiftListSharedContacts() {
    await this.authSvc.getToken().then((token) => {
      this.token = token;
    });
    return this.http.get(`${this.apiUrl}/SharedList/Contacts`, { headers: { 'Authorization': `bearer ${this.token}` } }).toPromise();
  }
}
