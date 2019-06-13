import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { GiftListService } from './services/gift-list.service';
import { GiftList, GiftItemQuery } from './models/gift-list';
import { GiftItem } from '../wish-list/models/gift-item';
import { Contact } from '../contacts/models/contact';
import { ContactService } from '../contacts/contact.service';

@Component({
  selector: 'app-gift-list',
  templateUrl: './gift-list.component.html',
  styleUrls: ['./gift-list.component.css']
})
export class GiftListComponent implements OnInit {

  public showCheckboxes = false;
  public hasGiftLists: boolean = false;
  public giftLists: GiftList[]
  public contacts: any[]
  public trashActionActive = false;
  public editActionActive = false;
  public addActionActive = false;
  public moveActionActive = false;
  public shareActionActive = false;
  public itemsToMove: GiftItem[];
  public contactsLoaded = false;
  public expandedLists: any[] = [];
  public expandedList: any;

  constructor(
    private glService: GiftListService,
    private cntctService: ContactService,
    private cd: ChangeDetectorRef,
  ) {
    this.glService.getLists().then((data) => {
      this.giftLists = data
      if (this.giftLists.length > 0) {
        this.hasGiftLists = true;
      }
    });

    this.cntctService.getUserContacts().then((contacts) => {
      this.contacts = contacts.map((contact: any) => {
        if (contact.contact != null)
          return contact.contact;
      });
      console.log();
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
        this.editActionActive = false;
        break;
      }
      // TODO: Make share only visible when a wishlist is selected.
      case "Share": {
        this.showCheckboxes = false;
        this.addActionActive = false;
        this.trashActionActive = false;
        this.moveActionActive = false;
        this.shareActionActive = true;
        this.editActionActive = false;
        break;
      }
      case "Move": {
        this.showCheckboxes = true;
        this.addActionActive = false;
        this.trashActionActive = false;
        this.moveActionActive = true;
        this.shareActionActive = false;
        this.editActionActive = false;
        break;
      }
      case "Delete": {
        this.showCheckboxes = true;
        this.trashActionActive = true;
        this.addActionActive = false;
        this.moveActionActive = false;
        this.shareActionActive = false;
        this.editActionActive = false;
        break;
      }
      case "Edit": {
        this.showCheckboxes = false;
        this.editActionActive = true;
        this.trashActionActive = false;
        this.addActionActive = false;
        this.moveActionActive = false;
        this.shareActionActive = false;
      }
    }
    this.cd.detectChanges();
  }

  public mobileActionClicked(actionInfo, content) {
    // If there's any modal open... close it.
    switch (actionInfo.action) {
      case "Add": {
        this.showCheckboxes = false;
        this.addActionActive = true;
        this.trashActionActive = false;
        this.moveActionActive = false;
        this.shareActionActive = false;
        this.editActionActive = false;
        break;
      }
      // TODO: Make share only visible when a wishlist is selected.
      case "Share": {
        this.showCheckboxes = false;
        this.addActionActive = false;
        this.trashActionActive = false;
        this.moveActionActive = false;
        this.shareActionActive = true;
        this.editActionActive = false;
        break;
      }
      case "Move": {
        this.showCheckboxes = true;
        this.addActionActive = false;
        this.trashActionActive = false;
        this.moveActionActive = true;
        this.shareActionActive = false;
        this.editActionActive = false;
        break;
      }
      case "Delete": {
        this.showCheckboxes = true;
        this.trashActionActive = true;
        this.addActionActive = false;
        this.moveActionActive = false;
        this.shareActionActive = false;
        this.editActionActive = false;
        break;
      }
      case "Edit": {
        this.showCheckboxes = false;
        this.editActionActive = true;
        this.trashActionActive = false;
        this.addActionActive = false;
        this.moveActionActive = false;
        this.shareActionActive = false;
      }
    }
    this.cd.detectChanges();
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
    // NOTE: As of 6/3 Edge does not support method flat();
    checkedItems = checkedItems.reduce((acc, val) => acc.concat(val), []);

    if (checkedItems.length == 0) {
      alert("You did not select any items. Please try again.")
      return false;
    }

    if (eventItem == null) {
      alert("You did not select a gift list");
      return false;
    }

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
        this.cd.detectChanges();
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

    // Update the expanded list array for certain list-actions.
    this.setExpandedListsArray();
    this.clearListActionDisplay();

    // Update the DOM
    this.cd.detectChanges();
  }

  public onListMoved(newList) {
    console.log(newList);
  }

  public getExpandedList() {
    if (this.expandedLists.length == 1) {
      return this.expandedLists[0]
    } else
      return null;
  }

  private setExpandedListsArray() {
    this.expandedLists = this.giftLists.filter(list => list.isExpanded == true);
  }

  private clearListActionDisplay() {
    if (this.expandedLists.length == 0 || this.expandedLists.length > 1) {
      this.showCheckboxes = false;
      this.moveActionActive = false;
      this.shareActionActive = false;
      this.editActionActive = false;
      this.trashActionActive = false;
    }
  }

  public tempRandomNumberGenerator(min, max) {
    return Math.floor(Math.random() * (max - min) + min);
  }
}
