import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListActionComponent } from './list-action.component';
import { ListActionItemComponent } from './list-action-item/list-action-item.component';
import { MoveToGiftListComponent } from './move-to-gift-list/move-to-gift-list.component';
import { DeleteWlItemComponent } from './delete-wl-item/delete-wl-item.component';

@NgModule({
  declarations: [
    ListActionComponent,
    ListActionItemComponent,
    MoveToGiftListComponent,
    DeleteWlItemComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ListActionComponent
  ]
})
export class ListActionModule { }
