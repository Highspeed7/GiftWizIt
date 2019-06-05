import { Component, OnInit, Input, OnChanges, SimpleChanges, ViewChild } from '@angular/core';
import { Contact } from 'src/app/contacts/models/contact';
import { ContactService } from 'src/app/contacts/contact.service';
import { GiftList } from 'src/app/gift-list/models/gift-list';
import { ListShare } from './models/list-share';
import { GiftListService } from 'src/app/gift-list/services/gift-list.service';
import { NgForm } from '@angular/forms';
import { SharedList } from 'src/app/shared-gift-list/models/shared-list';
import { ShareGiftListService } from './services/share-gift-list.service';

@Component({
  selector: 'gw-share-gift-list',
  templateUrl: './share-gift-list.component.html',
  styleUrls: ['./share-gift-list.component.css']
})
export class ShareGiftListComponent implements OnInit, OnChanges {
  @ViewChild('shareForm') shareForm: NgForm;
  @Input()
  public contacts: Contact[];

  @Input()
  public giftLists: GiftList[];

  public contactShares: any[] = null;

  public filteredListContacts: any[] = [];

  // Set to an appropriate model
  public selectedContacts: any[] = [];

  public selectedList: number;

  public sharedListPassword: string;

  public removedItemIndex: any;

  public dropdownList = [];

  public dropdownSettings = {};

  public contactsLoaded = false;

  public showPassField = true;

  constructor(
    private cntctSvc: ContactService,
    private gftSvc: GiftListService,
    private shareSvc: ShareGiftListService
  ) { }

  ngOnInit() {
    /* TODO: Make a change to where contacts don't appear if they've already had a given list shared with them. */
    // Then set the dropdown configuration.
    this.setDropdownSettings();
  }

  /* If for whatever reason, the contacts are not loaded
   * before the share button is clicked this will detect
   * them when they are, and load the dropdown config */
  ngOnChanges(changes: SimpleChanges) {
    // Detect when contacts are loaded
    if (changes.contacts) {
      // Filter the contacts again
      // this.filterContacts();
      // When contacts are changed...
      // Set the dropdown configuration
      this.setDropdownSettings();
    }
  }

  public async shareWithContactClicked() {
    // Build the object to send to api
    var listToShare = new ListShare();
    listToShare.g_list_id = this.selectedList;
    listToShare.password = this.sharedListPassword;
    listToShare.contacts = this.selectedContacts;
    console.log(listToShare);

    // Call api to share the list
    await this.gftSvc.shareList(listToShare).then((res) => {
      this.shareForm.reset();
      this.dropdownList = [];
      this.selectedContacts = [];
    });
  }

  public onItemSelect(item: any) {
    this.selectedContacts.push(item);
    console.log(this.selectedContacts);
  }

  public onItemDeselect(item: any) {
    this.getSelectedItemIndex(item.contactId);

    // Delete the deselected contact.
    delete this.selectedContacts[this.removedItemIndex];

    // Clean the selected contacts array.
    this.selectedContacts = this.selectedContacts.filter(function () { return true });
  }

  public onItemDeselectAll() {
    this.selectedContacts = [];
  }

  public onItemSelectAll(items: any[]) {
    this.selectedContacts = items;
  }

  public listChanged() {
    this.contactsLoaded = false;
    // Load contacts for the
    this.shareSvc.getGiftListSharedContacts().then((data: any[]) => {
      this.contactShares = data;
      this.filterContacts();
      this.setAnyListPassword();
      this.setDropdownSettings();
      this.contactsLoaded = true;
    });
  }

  private setAnyListPassword() {
    var listContacts = this.contactShares.filter((contact) => {
      if (contact.giftListId == this.selectedList) {
        return contact;
      }
    })

    if (listContacts.length > 0) {
      if (listContacts[0].password != null) {
        this.showPassField = false;
        // Set the password model
        this.sharedListPassword = listContacts[0].password;
      }
    }
  }

  private filterContacts() {
    // Filter the contactShares array on gift list id
    var listContacts = this.contactShares.filter((contact) => {
      if (contact.giftListId == this.selectedList) {
        return contact;
      }
    })

    // Make a copy
    this.filteredListContacts = this.contacts.slice(0);

    if (listContacts.length > 0) {
      for (var i = 0; i < this.filteredListContacts.length; i++) {
        listContacts.forEach((listContact) => {
          if (listContact.contact.email == this.filteredListContacts[i].email) {
            this.filteredListContacts.splice(i, 1);
          }
        });
      }
    }
  }

  private setDropdownSettings() {
    this.dropdownList = [];
    for (let c of this.filteredListContacts) {
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

  private getSelectedItemIndex(contactId) {
    this.selectedContacts.find((c, i) => {
      if (c.contactId == contactId) {
        this.removedItemIndex = i;
        return c;
      }
    });
  }
}
