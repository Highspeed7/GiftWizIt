import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { GiftListService } from './services/gift-list.service';
import { GiftList, GiftItemQuery } from './models/gift-list';
import { GiftItem } from '../wish-list/models/gift-item';

@Component({
  selector: 'app-gift-list',
  templateUrl: './gift-list.component.html',
  styleUrls: ['./gift-list.component.css']
})
export class GiftListComponent implements OnInit {

  public showCheckboxes = false;
  public hasGiftLists: boolean = false;
  public giftLists: GiftList[]
  public trashActionActive = false;
  public addActionActive = false;
  public moveActionActive = false;
  public shareActionActive = false;
  public itemsToMove: GiftItem[];

  constructor(
    private glService: GiftListService,
    private cd: ChangeDetectorRef
  ) {
    this.glService.getLists().then((data) => {
      this.giftLists = data
      if (this.giftLists.length > 0) {
        this.hasGiftLists = true;
      }
    });
  }

  ngOnInit() {}

  public onListAdded(e: GiftList) {
    this.glService.getLists().then((data) => {
      this.giftLists = data;
    });
  }

  public actionClicked(actionInfo) {
    // TODO: Add checkboxes to UI
    switch (actionInfo.action) {
      case "Add": {
        this.showCheckboxes = false;
        this.addActionActive = true;
        this.trashActionActive = false;
        this.moveActionActive = false;
        this.shareActionActive = false;
        break;
      }
      // TODO: Make share only visible when a wishlist is selected.
      case "Share": {
        this.addActionActive = false;
        this.trashActionActive = false;
        this.moveActionActive = false;
        this.shareActionActive = true;
        break;
      }
      case "Move": {
        this.showCheckboxes = true;
        this.addActionActive = false;
        this.trashActionActive = false;
        this.moveActionActive = true;
        this.shareActionActive = false;
        break;
      }
      case "Delete": {
        this.trashActionActive = true;
        this.addActionActive = false;
        this.moveActionActive = false;
        this.shareActionActive = false;
        break;
      }
    }
  }

  // TODO: Add a check for moves to same gift lists.
  public itemMoveClicked(eventItem) {
    var checkedItems: any[] = [];

    for (var i = 0; i < this.giftLists.length; i++) {
      checkedItems.push(this.giftLists[i].giftItems.filter((item) => {
        return item.checked === true;
      }));
    }

    // Flatten checkedItems array to remove empty slots
    checkedItems = checkedItems.flat();

    console.log(`Moving ${checkedItems} to ${eventItem}`);

    this.itemsToMove = checkedItems.map((item: GiftItemQuery) => {
      var giftItem: GiftItem = new GiftItem();
      giftItem.g_List_Id = item.gift_List_Id;
      giftItem.item_Id = item.item_Id;
      giftItem.to_Glist_Id = eventItem;
      return giftItem;
    });

    this.glService.moveItems(this.itemsToMove).then((res) => {
      this.glService.getLists().then((data) => {
        this.giftLists = data;
      })
    });
  }

  public itemSelected(item) {
    // Set the item's property to checked.
    if (item.checked == null || item.checked == false) {
      item.checked = true;
    } else {
      item.checked = false;
    }
  }

  public deleteList(list: GiftList) {
    this.glService.deleteList(list).subscribe((r) => {
      // If more than zero rows affected.
      if (r > 0) {
        // Get lists again.
        this.glService.getLists().then((data) => {
          this.giftLists = data;
        });
      }
    });
  }

  public async expandGiftList(list: GiftList) {
    // TODO: Implement nocache action so that items are updated appropriately and not stale.
    // Set the expanded property of a list.
    if (list.isExpanded == null) {
      await this.glService.getGiftItems(list.id).then((items: GiftItemQuery[]) => {
        list.giftItems = items;
      });
    }
    list.isExpanded = !list.isExpanded;
    // Update the DOM
    this.cd.detectChanges();
  }

  public onListMoved(newList) {
    console.log(newList);
  }
}
