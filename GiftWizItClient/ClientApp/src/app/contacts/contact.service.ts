import { Injectable } from '@angular/core';
import { AuthService } from '../authentication/services/auth.service';
import { HttpClient } from '@angular/common/http';
import { AddContactModel } from '../list-action/models/add-contact-model';
import { Contact } from './models/contact';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  private apiUrl = "https://localhost:44327/api"
  private token;
  constructor(private authSvc: AuthService, private http: HttpClient) { }

  public async getUserContacts(): Promise<Contact[]> {
    await this.authSvc.getToken().then((token) => {
      this.token = token;
    });
    return this.http.get<Contact[]>(`${this.apiUrl}/Contacts/Get`, { headers: { 'Authorization': `bearer ${this.token}` } })
      .toPromise();
  }

  public async addContact(contact: AddContactModel) {
    await this.authSvc.getToken().then((token) => {
      this.token = token;
    });
    this.http.post(`${this.apiUrl}/Contacts/Add`, contact, { headers: { 'Authorization': `bearer ${this.token}` } })
      .toPromise();
  }
}
