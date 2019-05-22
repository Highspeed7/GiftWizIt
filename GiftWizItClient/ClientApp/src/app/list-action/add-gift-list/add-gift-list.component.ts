import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MsalService } from '@azure/msal-angular';
import { GiftList } from 'src/app/gift-list/models/gift-list';
import { GiftListService } from 'src/app/gift-list/services/gift-list.service';

@Component({
  selector: 'gw-add-gift-list',
  templateUrl: './add-gift-list.component.html',
  styleUrls: ['./add-gift-list.component.css']
})
export class AddGiftListComponent implements OnInit {
  @Output("onListAdded")
  public onListAdded = new EventEmitter<GiftList>();
  public addListForm: FormGroup;

  constructor(
    private msal: MsalService,
    private glService: GiftListService
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

    this.glService.createList(glist).then((res) => {
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
