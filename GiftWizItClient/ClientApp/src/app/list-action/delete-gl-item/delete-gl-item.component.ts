import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'gw-delete-gl-item',
  templateUrl: './delete-gl-item.component.html',
  styleUrls: ['./delete-gl-item.component.css']
})
export class DeleteGlItemComponent implements OnInit {
  @Output()
  public onDeleteItemClicked: EventEmitter<any> = new EventEmitter();

  @Output()
  public onDeclineDelete: EventEmitter<any> = new EventEmitter();

  @Input()
  public itemsCheckedCount: number = 0;

  @Input()
  public giftLists: any;

  constructor() { }

  ngOnInit() {
  }

  public confirmDelete() {
    this.onDeleteItemClicked.emit();
  }

  public cancelDelete() {
    this.onDeclineDelete.emit();
  }
}
