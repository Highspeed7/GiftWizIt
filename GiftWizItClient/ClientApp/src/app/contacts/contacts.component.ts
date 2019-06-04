import { Component, OnInit } from '@angular/core';
import { AddContactModel } from '../list-action/models/add-contact-model';
import { ContactService } from './contact.service';
import { Contact } from './models/contact';

@Component({
  selector: 'gw-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.css']
})
export class ContactsComponent implements OnInit {

  public addActionActive: boolean = false;
  public trashActionActive: boolean = false;
  public showCheckboxes: boolean = false;

  public contacts: Contact[];

  constructor(private contactSvc: ContactService) { }

  ngOnInit() {
    this.contactSvc.getUserContacts().then((data: Contact[]) => {
      this.contacts = data;
    });
  }

  public actionClicked(actionInfo) {
    switch (actionInfo.action) {
      case "Add": {
        this.showCheckboxes = false;
        this.addActionActive = true;
        this.trashActionActive = false;
        break;
      }
      case "Trash": {
        this.showCheckboxes = true;
        this.trashActionActive = true;
        this.addActionActive = false;
        break;
      }
    }
  }

  public contactAdded(contact: AddContactModel) {
    this.contactSvc.addContact(contact).then((res) => {
      this.contactSvc.getUserContacts().then((data: Contact[]) => {
        this.contacts = data;
      });
    });
  }
}
