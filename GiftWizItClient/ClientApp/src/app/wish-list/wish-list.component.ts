import { Component, OnInit } from '@angular/core';
import { WishListService } from './services/wish-list.service';
import { WishList } from './models/wish-list';
import { GiftListService } from '../gift-list/services/gift-list.service';
import { GiftList } from '../gift-list/models/gift-list';
import { GiftItem } from './models/gift-item';
import { WishItemService } from './services/wish-item.service';

@Component({
  selector: 'app-wish-list',
  templateUrl: './wish-list.component.html',
  styleUrls: ['./wish-list.component.css']
})
export class WishListComponent implements OnInit {

  public showCheckboxes: boolean = false;
  public wishList: WishList[];
  public giftLists: GiftList[];
  public itemsToMove: GiftItem[];

  constructor(
    private wshSvc: WishListService,
    private wshItmSvc: WishItemService,
    private glstSvc: GiftListService,
  ) { }

  ngOnInit() {
    this.glstSvc.getLists().subscribe((data) => {
      this.giftLists = data;
    });
    this.wshSvc.getLists()
      .subscribe((data: WishList[]) => {
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

  public itemSelected(item: WishList) {
    // Set the item's property to checked.
    if (item.checked == null || item.checked == false) {
      item.checked = true;
    } else {
      item.checked = false;
    }
  }

  public itemMoveClicked(eventItem) {
    var checkedItems = this.wishList.filter((item) => {
      return item.checked === true;
    });

    this.itemsToMove = checkedItems.map((item: WishList) => {
      var giftItem: GiftItem = new GiftItem();
      giftItem.g_List_Id = parseInt(eventItem);
      giftItem.item_Id = item.item_Id;
      return giftItem;
    });

    this.wshItmSvc.moveItems(this.itemsToMove).subscribe((res) => {
      this.wshSvc.getLists().subscribe((data) => {
        this.wishList = data;
      });
    });
  }
}
