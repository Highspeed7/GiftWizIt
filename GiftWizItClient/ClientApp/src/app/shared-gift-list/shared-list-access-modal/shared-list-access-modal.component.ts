import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'gw-shared-list-access-modal',
  templateUrl: './shared-list-access-modal.component.html',
  styleUrls: ['./shared-list-access-modal.component.css']
})
export class SharedListAccessModalComponent implements OnInit {

  public gListPass: string;

  constructor() { }

  ngOnInit() {
  }

}
