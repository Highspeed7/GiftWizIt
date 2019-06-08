import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { GiftList } from 'src/app/gift-list/models/gift-list';
import { NgForm } from '@angular/forms';
import { DialogConfig } from 'src/app/dialog/dialog-config';

@Component({
  selector: 'gw-move-to-gift-list',
  templateUrl: './move-to-gift-list.component.html',
  styleUrls: ['./move-to-gift-list.component.css']
})
export class MoveToGiftListComponent implements OnInit {
  @ViewChild('moveForm') moveForm: NgForm;

  @Output()
  onMoveClicked: EventEmitter<any> = new EventEmitter<any>();

  @Input()
  public giftLists: GiftList[];
  public selectedGiftList: string;

  constructor(private dialogCfg: DialogConfig) {
    if (this.dialogCfg.data != null) {
      this.giftLists = this.dialogCfg.data.giftLists;
      this.onMoveClicked = this.dialogCfg.data.onMoveClicked;
    }
  }

  ngOnInit() {
    console.log(`Move to list initiated`);
  }

  public moveItemsClicked() {
    this.onMoveClicked.emit(this.selectedGiftList);
    this.moveForm.reset();
  }
}
