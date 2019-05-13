import { Component, OnInit } from '@angular/core';
import { WishListService } from './services/wish-list.service';
import { WishList } from './models/wish-list';

@Component({
  selector: 'app-wish-list',
  templateUrl: './wish-list.component.html',
  styleUrls: ['./wish-list.component.css']
})
export class WishListComponent implements OnInit {

  public showCheckboxes: boolean = false;
  public wishList: WishList[];

  constructor(
    private wshSvc: WishListService
  ) { }

  ngOnInit() {
    // TODO: Move to service.
    this.wshSvc.getLists()
      .subscribe((data) => {
        this.wishList = data;
      });
  }

  public actionClicked(actionInfo) {
    this.showCheckboxes = true;
    switch (actionInfo.action) {
      case "Cancel":
        this.showCheckboxes = false;
        break;
    }
  }
}
