import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WishListComponent } from './wish-list.component';
import { RouterModule } from '@angular/router';
import { WishListService } from './services/wish-list.service';
import { HttpModule } from '@angular/http';
import { GiftTagComponent } from '../gift-tag/gift-tag.component';
import { ListActionModule } from '../list-action/list-action.module';
import { WishItemService } from './services/wish-item.service';
import { MsalGuard } from '@azure/msal-angular';

@NgModule({
  declarations: [
    WishListComponent,
    GiftTagComponent
],
  imports: [
    CommonModule,
    HttpModule,
    ListActionModule,
    RouterModule.forRoot([
      {
        path: "wish-list",
        component: WishListComponent,
        canActivate: [MsalGuard]
      }
    ])
  ],
  providers: [WishListService, WishItemService]
})
export class WishListModule { }
