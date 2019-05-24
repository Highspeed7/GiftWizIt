import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AddContactModel } from '../models/add-contact-model';

@Component({
  selector: 'gw-add-contact',
  templateUrl: './add-contact.component.html',
  styleUrls: ['./add-contact.component.css']
})
export class AddContactComponent implements OnInit {

  @Output()
  public onContactAdded = new EventEmitter<AddContactModel>();

  public addContactForm: FormGroup;

  private addedContact: AddContactModel = new AddContactModel();

  constructor() { }

  ngOnInit() {
    this.addContactForm = new FormGroup({
      "name": new FormControl("", [Validators.required]),
      "email" : new FormControl("", [Validators.required])
    });
  }

  public onAddContact() {
    this.addedContact.name = this.addContactForm.controls.name.value;
    this.addedContact.email = this.addContactForm.controls.email.value;
    this.onContactAdded.emit(this.addedContact);
  }
}
