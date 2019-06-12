import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  private _notifications: BehaviorSubject<Notification> = new BehaviorSubject<Notification>(null);
  public notifications$ = this._notifications.asObservable();
  
  constructor() { }
}
