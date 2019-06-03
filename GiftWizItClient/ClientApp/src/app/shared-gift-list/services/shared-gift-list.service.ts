import { Injectable } from '@angular/core';
import { AuthService } from 'src/app/authentication/services/auth.service';
import { HttpClient } from '@angular/common/http';
import * as env from '../../../environments/environment';
import { WindowRefService } from 'src/app/window-ref.service';
import { GuestInfo, GuestList } from 'src/app/models/guestInfo';
import { Contact } from 'src/app/contacts/models/contact';
import { Utilities } from 'src/app/utils/Utilities';
import { GWAppConstants } from 'src/app/constants/appConstants';
import { SharedList } from '../models/shared-list';

@Injectable({
  providedIn: 'root'
})
export class SharedGiftListService {
  private apiUrl = env.environment.apiUrl;
  private enteredPassword: string;
  private access;
  private window: any;
  private guestInfo: GuestInfo;
  private utils: Utilities;

  constructor(
    private authSvc: AuthService,
    private windowRef: WindowRefService,
    private http: HttpClient
  ) {
    this.window = this.windowRef.nativeWindow;
    this.utils = new Utilities();
  }

  public getSharedList(giftListId, giftListPassword) {
    return this.http.post(`${this.apiUrl}/SharedList`, { gListId: giftListId, gListPass: giftListPassword }).toPromise()
  }

  public async buildNonRegisteredUserX(emailId: string, giftListId: string) {
    // TODO: This does a database, write... we want to do a read instead.
    await this.http.post(`${this.apiUrl}/Contacts/EmailVerify?emailId=${emailId}`, null)
      .toPromise()
      .then((contact: Contact) => {
        this.guestInfo = new GuestInfo();
        this.guestInfo.name = contact.name;
        this.guestInfo.email = contact.email;
        
        // Store the guestInfo in localstorage
        this.window.localStorage.setItem(GWAppConstants.strGuestInfo, JSON.stringify(this.guestInfo));
      });
    return;
  }

  public storeListToUserX(list: SharedList, listPass: string) {
    var currTimeSeconds = this.utils.getCurrTimeInSeconds();
    this.guestInfo = JSON.parse(this.window.localStorage.getItem(GWAppConstants.strGuestInfo));
    this.guestInfo.lists.push({
      giftListName: list.giftList.name,
      giftListId: list.giftList.id,
      giftListPass: listPass,
      storedTime: currTimeSeconds,
      dataExpireTime: currTimeSeconds + GWAppConstants.strGuestListExpiryTimeSecs
    });
    // Put the object back
    this.window.localStorage.setItem(GWAppConstants.strGuestInfo, JSON.stringify(this.guestInfo));
  }

  public checkStoredLists(prevLists: GuestList[], giftListId: string) {
    if (prevLists != null) {
      var listFound = prevLists.filter((list: GuestList) => {
        return list.giftListId == parseInt(giftListId);
      })
      if (listFound.length == 0) {
        return false;
      }
      return true;
    }
  }
}
