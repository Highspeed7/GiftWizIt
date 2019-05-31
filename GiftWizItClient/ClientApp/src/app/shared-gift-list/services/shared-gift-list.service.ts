import { Injectable } from '@angular/core';
import { AuthService } from 'src/app/authentication/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class SharedGiftListService {

  constructor(private authSvc: AuthService) { }

  public getSharedList(giftListId, giftListPassword) {

  }
}
