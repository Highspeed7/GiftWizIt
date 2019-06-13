import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { GiftList } from 'src/app/gift-list/models/gift-list';
import { EditListModel } from './models/edit-contact';

@Component({
  selector: 'gw-edit-gift-list',
  templateUrl: './edit-gift-list.component.html',
  styleUrls: ['./edit-gift-list.component.css']
})
export class EditGiftListComponent implements OnInit {
  @Output()
  onEditListClicked: EventEmitter<EditListModel> = new EventEmitter();

  @Input()
  public giftLists: GiftList[];
  public expandedList: GiftList[];
  public newName: string;
  public newPassword: string;

  constructor() { }

  ngOnInit() {
    this.expandedList = this.giftLists.filter((list) => {
      return list.isExpanded == true;
    });

    if (this.expandedList.length > 0) {
      this.newName = this.expandedList[0].name;
    }
  }

  public editListClicked() {
    var editedList = new EditListModel();
    editedList.giftListId = this.expandedList[0].id;
    editedList.newName = this.newName;
    editedList.newPass = this.newPassword;

    this.onEditListClicked.emit(editedList);
  }
}
