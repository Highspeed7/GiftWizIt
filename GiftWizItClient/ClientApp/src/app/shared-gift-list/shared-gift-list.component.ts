import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Utilities } from '../utils/Utilities';
import { GiftListService } from '../gift-list/services/gift-list.service';
import { GiftList } from '../gift-list/models/gift-list';
import { DialogService } from '../dialog/dialog.service';
import { SharedListAccessModalComponent } from './shared-list-access-modal/shared-list-access-modal.component';

@Component({
  selector: 'gw-shared-gift-list',
  templateUrl: './shared-gift-list.component.html',
  styleUrls: ['./shared-gift-list.component.css']
})
export class SharedGiftListComponent implements OnInit {

  public giftList: GiftList;
  private utilities: Utilities;
  private giftListId: string;
  private emailId: string;

  constructor(
    private route: ActivatedRoute,
    private gftSvc: GiftListService,
    private dialog: DialogService
  ) {
    this.utilities = new Utilities();
    this.dialog.open(SharedListAccessModalComponent, null);
  }

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      if (!this.utilities.isEmpty(params)) {
        if (params["gListId"] != null) {
          this.giftListId = params["gListId"];
        }
        if (params["emailId"] != null) {
          this.emailId = params["emailId"];
        }
      }
    });
    // Get the list
    // Personalize the visiting user's experience.
  }
}
