import { Component, OnInit, Input } from '@angular/core';
import { GiftList } from 'src/app/gift-list/models/gift-list';

@Component({
  selector: 'gw-move-to-gift-list',
  templateUrl: './move-to-gift-list.component.html',
  styleUrls: ['./move-to-gift-list.component.css']
})
export class MoveToGiftListComponent implements OnInit {

  @Input()
  public giftLists: GiftList[];

  constructor() {
  }

  ngOnInit() {
    //this.getLists();
  }
}
