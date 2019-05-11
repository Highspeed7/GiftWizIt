import { Component, OnInit } from '@angular/core';
import { WishListService } from './services/wish-list.service';
import { WishList } from './models/wish-list';

@Component({
  selector: 'app-wish-list',
  templateUrl: './wish-list.component.html',
  styleUrls: ['./wish-list.component.css']
})
export class WishListComponent implements OnInit {

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

}
