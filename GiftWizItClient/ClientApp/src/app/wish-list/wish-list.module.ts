import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WishListComponent } from './wish-list.component';
import { AuthGuard } from '../authentication/guards/auth.guard';
import { RouterModule } from '@angular/router';
import { WishListService } from './services/wish-list.service';
import { HttpModule } from '@angular/http';
import { GiftTagComponent } from '../gift-tag/gift-tag.component';

@NgModule({
  declarations: [
    WishListComponent,
    GiftTagComponent
],
  imports: [
    CommonModule,
    HttpModule,
    RouterModule.forRoot([
      {
        path: "wish-list",
        component: WishListComponent,
        canActivate: [AuthGuard]
      }
    ])
  ],
  providers: [WishListService]
})
export class WishListModule { }
