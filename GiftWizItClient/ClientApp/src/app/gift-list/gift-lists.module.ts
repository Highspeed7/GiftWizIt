import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GiftListComponent } from './gift-list.component';
import { GiftListService } from './services/gift-list.service';
import { RouterModule } from '@angular/router';
import { AuthGuard } from '../authentication/guards/auth.guard';
import { CreateGiftListComponent } from './create-gift-list/create-gift-list.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    GiftListComponent,
    CreateGiftListComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      {
        path: "gift-lists",
        component: GiftListComponent,
        canActivate: [AuthGuard]
      }
    ])
  ],
  providers: [GiftListService]
})
export class GiftListsModule { }
