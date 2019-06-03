import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Utilities } from '../utils/Utilities';
import { GiftListService } from '../gift-list/services/gift-list.service';
import { GiftList } from '../gift-list/models/gift-list';
import { DialogService } from '../dialog/dialog.service';
import { SharedListAccessModalComponent } from './shared-list-access-modal/shared-list-access-modal.component';
import { SharedGiftListService } from './services/shared-gift-list.service';
import { SharedList } from './models/shared-list';
import { WindowRefService } from '../window-ref.service';
import { AuthService } from '../authentication/services/auth.service';
import { GuestInfo, GuestList } from '../models/guestInfo';

@Component({
  selector: 'gw-shared-gift-list',
  templateUrl: './shared-gift-list.component.html',
  styleUrls: ['./shared-gift-list.component.css']
})
export class SharedGiftListComponent implements OnInit {

  public sharedList: SharedList = null;
  public prevViewedLists: any[] = [];
  private utilities: Utilities;
  private giftListId: string;
  private emailId: string;
  private window: any;
  private dialogRef;
  private guestUserInfo: GuestInfo;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private listSvc: SharedGiftListService,
    private dialog: DialogService,
    private authSvc: AuthService,
    private windowRef: WindowRefService,
    private shrdLstSvc: SharedGiftListService
  ) {
    this.window = this.windowRef.nativeWindow;
    this.utilities = new Utilities();
   

    this.route.queryParams.subscribe(async (params) => {
      if (!this.utilities.isEmpty(params)) {
        if (params["gListId"] != null) {
          this.giftListId = params["gListId"];
         
        }
        if (params["emailId"] != null) {
          this.emailId = params["emailId"];

          // Check for guest user experience
          this.guestUserInfo = this.authSvc.getNonRegisteredUserX();
          if (this.guestUserInfo != null) {
            this.prevViewedLists = this.guestUserInfo.lists;
          } else {
            await this.listSvc.buildNonRegisteredUserX(this.emailId, this.giftListId);
          }

          // We need to check if the list exists in the user context
          if (this.prevViewedLists.length > 0) {
            if (!this.listSvc.checkStoredLists(this.prevViewedLists, this.giftListId)) {
              this.runAccessDialog();
            }
          } else {
            this.runAccessDialog();
          }
        }
      }
    });
  }

  public prevListSelected(list: GuestList) {
    this.listSvc.getSharedList(list.giftListId, list.giftListPass).then(async (list: SharedList) => {
      if (list != null) {
        this.sharedList = list;
      }
    });
  }

  //public viewAnotherList() {
  //  this.runAccessDialog();
  //}

  private async runAccessDialog() {
    this.dialogRef = this.dialog.open(SharedListAccessModalComponent, {
      data: {
        giftListId: this.giftListId
      }
    });

    this.dialogRef.afterClosed.subscribe(result => {
      if (result == null) {
        if (this.prevViewedLists.length == 0) {
          this.router.navigate(["/"]);
        }
      } else {
        // Make api call
        this.listSvc.getSharedList(this.giftListId, result).then(async (list: SharedList) => {
          if (list != null) {
            this.sharedList = list;
            // TODO: IMPORTANT - Make it so when the list is expired the entire guest info is cleared.
            if (!this.listSvc.checkStoredLists(this.prevViewedLists, this.giftListId)) {
              await this.listSvc.storeListToUserX(list, result);
            }
          } else {
            this.runAccessDialog();
          }
        });
      }
    });
  }

  ngOnInit() {
    // Personalize the visiting user's experience.
  }
}
