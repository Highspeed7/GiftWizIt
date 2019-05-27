import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Contact } from 'src/app/contacts/models/contact';
import { ContactService } from 'src/app/contacts/contact.service';

@Component({
  selector: 'gw-share-gift-list',
  templateUrl: './share-gift-list.component.html',
  styleUrls: ['./share-gift-list.component.css']
})
export class ShareGiftListComponent implements OnInit, OnChanges {

  @Input()
  public contacts: Contact[];

  // Set to an appropriate model
  public selectedContacts: any[];

  public dropdownList = [];

  public dropdownSettings = {};

  constructor(private cntctSvc: ContactService) { }

  ngOnInit() {
    // Get the contacts for the list
    console.log(this.contacts);
      // Then set the dropdown configuration.
  }

  /* If for whatever reason, the contacts are not loaded
   * before the share button is clicked this will detect
   * them when they are, and load the dropdown config */
  ngOnChanges(changes: SimpleChanges) {
    // Detect when contacts are loaded
    if (changes.contacts) {
      console.log(changes.contacts);
      // When contacts are changed...
      // Set the dropdown configuration
      this.setDropdownSettings();
    }
  }

  public shareWithContactClicked() {
    // console.log(selectedContacts);
  }

  private setDropdownSettings() {
    for (let c of this.contacts) {
      this.dropdownList.push({
        contactId: c.contactId,
        name: c.name
      });
    }
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'contactId',
      textField: 'name',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true
    };
  }

  public onItemSelect(item: any) {
    this.selectedContacts.push(item);
    console.log(this.selectedContacts);
  }
}
