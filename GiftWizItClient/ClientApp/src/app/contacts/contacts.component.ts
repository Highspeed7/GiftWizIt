import { Component, OnInit } from '@angular/core';
import { AddContactModel } from '../list-action/models/add-contact-model';
import { ContactService } from './contact.service';

@Component({
  selector: 'gw-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.css']
})
export class ContactsComponent implements OnInit {

  public addActionActive: boolean = false;
  public trashActionActive: boolean = false;

  constructor(private contactSvc: ContactService) { }

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

  public contactAdded(contact: AddContactModel) {
    this.contactSvc.addContact(contact).then((res) => {
      console.log("Added contact response " + res);
    });
  }
}
