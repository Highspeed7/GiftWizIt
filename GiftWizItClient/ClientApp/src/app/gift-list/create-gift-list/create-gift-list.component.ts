import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { GiftListService } from '../services/gift-list.service';
import { GiftList } from '../models/gift-list';
import { MsalService } from '@azure/msal-angular';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-gift-list',
  templateUrl: './create-gift-list.component.html',
  styleUrls: ['./create-gift-list.component.css']
})
export class CreateGiftListComponent implements OnInit {
  @Output("onListAdded")
  public onListAdded = new EventEmitter<GiftList>();

  public addListForm: FormGroup;

  constructor(
    private glService: GiftListService,
    private msal: MsalService,
    private router: Router
  ) { }

  ngOnInit() {
    this.addListForm = new FormGroup({
      'name': new FormControl('', [Validators.required])
    });
  }

  public onCreateList() {
    var name = this.addListForm.get("name").value;
    var userId: any = this.msal.getUser();

    var glist = new GiftList(
      name,
      userId.idToken.oid
    );

    this.glService.createList(glist).subscribe((res) => {
      console.log(res)
      if (res !== null) {
        this.addListForm.reset();
        this.listAdded(glist);
      }
    });
  }

  public listAdded(glist: GiftList) {
    this.onListAdded.emit(glist);
  }
}
