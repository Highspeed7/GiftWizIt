import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { HubConnectionBuilder } from '@aspnet/signalr';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../authentication/services/auth.service';
import * as env from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  private apiUrl: string = env.environment.apiUrl;
  private _notifications: BehaviorSubject<Notification> = new BehaviorSubject<Notification>(null);
  private _hub: HubConnectionBuilder;
  private token;
  public notifications$ = this._notifications.asObservable();

  constructor(
    private http: HttpClient,
    private authSvc: AuthService
  ) {
    this._hub = new HubConnectionBuilder();
  }

  public connect() {
    var url = "https://localhost:44327/notifHub"

    var connection = this._hub.withUrl(url).build();
    var connectionId;

    connection.on("Notification", (message) => {
      alert(message);
    });

    connection.start().then(() => {
      connection.invoke("getConnectionId").then((id) => {
        connectionId = id;
        this.authSvc.getToken().then((token) => {
          this.token = token;
          this.http.post(`${this.apiUrl}/NotificationsChannel?connectionId=${connectionId}`, null, { headers: { 'Authorization': `bearer ${this.token}` } }).subscribe();
        });
      });
      // Get the token
      
    }).catch(err => console.log(err.toString()));

    //this.http.post()
  }
}
