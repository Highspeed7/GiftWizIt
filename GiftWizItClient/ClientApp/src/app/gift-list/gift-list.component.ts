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

  constructor(private glService: GiftListService) {
    this.glService.getLists().subscribe((data) => {
      this.giftLists = data
      if (this.giftLists.length > 0) {
        this.hasGiftLists = true;
      }
    });
  }

  ngOnInit() {}

  public onListAdded(e: GiftList) {
    this.glService.getLists().subscribe((data) => {
      this.giftLists = data;
    });
  }

  public deleteList(list: GiftList) {
    this.glService.deleteList(list).subscribe((r) => {
      // If more than zero rows affected.
      if (r > 0) {
        // Get lists again.
        this.glService.getLists().subscribe((data) => {
          this.giftLists = data;
        });
      }
    });
  }
}
