import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { GiftListService } from './services/gift-list.service';
import { GiftList, GiftItemQuery } from './models/gift-list';

@Component({
  selector: 'app-gift-list',
  templateUrl: './gift-list.component.html',
  styleUrls: ['./gift-list.component.css']
})
export class GiftListComponent implements OnInit {

  public hasGiftLists: boolean = false;
  public giftLists: GiftList[]
  public trashActionActive = false;
  public addActionActive = false;

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
        this.addActionActive = true;
        this.trashActionActive = false;
        break;
      }
      case "Delete": {
        this.trashActionActive = true;
        this.addActionActive = false;
        break;
      }
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
}
