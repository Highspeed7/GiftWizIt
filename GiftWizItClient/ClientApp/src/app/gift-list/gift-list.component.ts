import { Component, OnInit } from '@angular/core';
import { GiftListService } from './services/gift-list.service';
import { GiftList } from './models/gift-list';

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

  constructor(private glService: GiftListService) {
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
}
