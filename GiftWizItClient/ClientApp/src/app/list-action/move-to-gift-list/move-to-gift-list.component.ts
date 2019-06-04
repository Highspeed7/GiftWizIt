import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { GiftList } from 'src/app/gift-list/models/gift-list';
import { NgForm } from '@angular/forms';

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

  constructor() {
  }

  ngOnInit() {
  }

  public moveItemsClicked() {
    this.onMoveClicked.emit(this.selectedGiftList);
    this.moveForm.reset();
  }
}
