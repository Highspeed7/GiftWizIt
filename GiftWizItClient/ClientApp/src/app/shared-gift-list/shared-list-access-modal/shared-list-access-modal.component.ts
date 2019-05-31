import { Component, OnInit } from '@angular/core';
import { DialogConfig } from 'src/app/dialog/dialog-config';
import { DialogRef } from 'src/app/dialog/dialog-ref';

@Component({
  selector: 'gw-shared-list-access-modal',
  templateUrl: './shared-list-access-modal.component.html',
  styleUrls: ['./shared-list-access-modal.component.css']
})
export class SharedListAccessModalComponent implements OnInit {

  public gListPass: string;

  constructor(public dialog: DialogRef) { }

  ngOnInit() {
  }

  public onClose() {
    this.dialog.close();
  }
}
