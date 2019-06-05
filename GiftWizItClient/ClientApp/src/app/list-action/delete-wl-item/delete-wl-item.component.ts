import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'gw-delete-wl-item',
  templateUrl: './delete-wl-item.component.html',
  styleUrls: ['./delete-wl-item.component.css']
})
export class DeleteWlItemComponent implements OnInit {
  @Output()
  public onDeleteConfirm: EventEmitter<any> = new EventEmitter();

  @Output()
  public onDeleteCancel: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  onDeleteConfirmed() {
    this.onDeleteConfirm.emit();
  }

  onDeleteCancelled() {
    this.onDeleteCancel.emit();
  }

}
