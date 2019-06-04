import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AddContactModel } from '../models/add-contact-model';
import { Contact } from 'src/app/contacts/models/contact';

@Component({
  selector: 'gw-add-contact',
  templateUrl: './add-contact.component.html',
  styleUrls: ['./add-contact.component.css']
})
export class AddContactComponent implements OnInit {

  @Output()
  public onContactAdded = new EventEmitter<AddContactModel>();

  @Input()
  public contacts: any[];

  public addContactForm: FormGroup;

  private addedContact: AddContactModel = new AddContactModel();

  constructor() { }

  ngOnInit() {
    this.addContactForm = new FormGroup({
      "name": new FormControl("", [Validators.required]),
      "email": new FormControl("", [Validators.required, Validators.email, this.checkContactEmail.bind(this)])
    });
  }

  public onAddContact() {
    this.addedContact.name = this.addContactForm.controls.name.value;
    this.addedContact.email = this.addContactForm.controls.email.value;
    this.onContactAdded.emit(this.addedContact);
  }

  private checkContactEmail(control: FormControl): { [s: string]: boolean } {
    var existingEmails = this.contacts.filter((contact) => {
      return contact.contact.email == control.value;
    })
    console.log(this.addContactForm);
    return (existingEmails.length > 0) ? { 'emailExists': true } : null;
  }
}
