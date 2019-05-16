import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GiftListComponent } from './gift-list.component';
import { GiftListService } from './services/gift-list.service';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { MsalGuard } from '@azure/msal-angular';
import { ListActionModule } from '../list-action/list-action.module';

@NgModule({
  declarations: [
    GiftListComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ListActionModule,
    RouterModule.forRoot([
      {
        path: "gift-lists",
        component: GiftListComponent,
        canActivate: [MsalGuard]
      }
    ])
  ],
  providers: [GiftListService]
})
export class GiftListsModule { }
