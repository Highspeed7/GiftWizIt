import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'gw-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.css']
})
export class ContactsComponent implements OnInit {

  public addActionActive: boolean = false;
  public trashActionActive: boolean = false;

  constructor() { }

  ngOnInit() {
  }

  public actionClicked(actionInfo) {
    switch (actionInfo.action) {
      case "Add": {
        this.addActionActive = true;
        this.trashActionActive = false;
        break;
      }
      case "Trash": {
        this.trashActionActive = true;
        this.addActionActive = false;
        break;
      }
    }
  }

}
