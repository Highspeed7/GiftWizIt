import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedGiftListComponent } from './shared-gift-list.component';
import { SharedGiftListService } from './services/shared-gift-list.service';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SharedListAccessModalComponent } from './shared-list-access-modal/shared-list-access-modal.component';

@NgModule({
  declarations: [
    SharedGiftListComponent,
    SharedListAccessModalComponent
  ],
  imports: [
    FormsModule,
    CommonModule,
    RouterModule.forRoot([
      {
        path: "shared-gift-list",
        component: SharedGiftListComponent
      }
    ])
  ],
  providers: [
    SharedGiftListService
  ]
})
export class SharedGiftListModule { }
