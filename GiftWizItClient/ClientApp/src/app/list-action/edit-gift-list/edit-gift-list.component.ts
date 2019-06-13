import { Component, OnInit, Input } from '@angular/core';
import { GiftList } from 'src/app/gift-list/models/gift-list';

@Component({
  selector: 'gw-edit-gift-list',
  templateUrl: './edit-gift-list.component.html',
  styleUrls: ['./edit-gift-list.component.css']
})
export class EditGiftListComponent implements OnInit {

  @Input()
  public giftLists: GiftList[];
  public expandedList: GiftList[];
  public newName: string;

  constructor() { }

  ngOnInit() {
    this.expandedList = this.giftLists.filter((list) => {
      return list.isExpanded == true;
    });

    if (this.expandedList.length > 0) {
      this.newName = this.expandedList[0].name;
    }
  }
}
