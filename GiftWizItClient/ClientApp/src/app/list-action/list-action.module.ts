import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListActionComponent } from './list-action.component';
import { ListActionItemComponent } from './list-action-item/list-action-item.component';
import { MoveToGiftListComponent } from './move-to-gift-list/move-to-gift-list.component';
import { DeleteWlItemComponent } from './delete-wl-item/delete-wl-item.component';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    ListActionComponent,
    ListActionItemComponent,
    MoveToGiftListComponent,
    DeleteWlItemComponent
  ],
  imports: [
    NgbModule.forRoot(),
    CommonModule,
    FormsModule
  ],
  exports: [
    ListActionComponent
  ]
})
export class ListActionModule { }
