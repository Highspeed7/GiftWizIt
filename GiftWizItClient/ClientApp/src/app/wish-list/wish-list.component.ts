import { Component, OnInit } from '@angular/core';
import { WishListService } from './services/wish-list.service';
import { WishList } from './models/wish-list';
import { GiftListService } from '../gift-list/services/gift-list.service';
import { GiftList } from '../gift-list/models/gift-list';

@Component({
  selector: 'app-wish-list',
  templateUrl: './wish-list.component.html',
  styleUrls: ['./wish-list.component.css']
})
export class WishListComponent implements OnInit {

  public showCheckboxes: boolean = false;
  public wishList: WishList[];
  public giftLists: GiftList[];

  constructor(
    private wshSvc: WishListService,
    private glstSvc: GiftListService,
  ) { }

  ngOnInit() {
    this.glstSvc.getLists().subscribe((data) => {
      this.giftLists = data;
    });
    this.wshSvc.getLists()
      .subscribe((data) => {
        this.wishList = data;
      });
  }

  public actionClicked(actionInfo) {
    this.showCheckboxes = true;
    switch (actionInfo.action) {
      case "Cancel":
        this.showCheckboxes = false;
        break;
    }
  }

  //public async getLists() {
  //  try {
  //    this.glstSvc.getLists().subscribe((data) => {
  //      this.giftLists = data;
  //      //this.cd.detectChanges();
  //    });
  //  } catch (e) {
  //    // Terrible implementation
  //    // TODO: Fix login process to match that of browser extension
  //    if (e.toString().indexOf('token') > -1) {
  //      //this.acntSvc.loggedIn$.subscribe((value) => {
  //      //  if (value == true) {
  //      //    this.gftSvc.getLists().subscribe((data) => {
  //      //      this.giftLists = data;
  //      //      this.cd.detectChanges();
  //      //    })
  //      //  } else {
  //      //    throw e;
  //      //  }
  //      //});
  //      this.authSvc.login();
  //    }
  //  }
  //}
}
