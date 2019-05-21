import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'gw-add-contact',
  templateUrl: './add-contact.component.html',
  styleUrls: ['./add-contact.component.css']
})
export class AddContactComponent implements OnInit {

  public addContactForm: FormGroup;

  constructor() { }

  ngOnInit() {
    this.addContactForm = new FormGroup({
      "name": new FormControl("", [Validators.required]),
      "email" : new FormControl("", [Validators.required])
    });
  }

}
