import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListActionComponent } from './list-action.component';
import { ListActionItemComponent } from './list-action-item/list-action-item.component';
import { MoveToGiftListComponent } from './move-to-gift-list/move-to-gift-list.component';
import { DeleteWlItemComponent } from './delete-wl-item/delete-wl-item.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AddGiftListComponent } from './add-gift-list/add-gift-list.component';
import { AddContactComponent } from './add-contact/add-contact.component';

@NgModule({
  declarations: [
    ListActionComponent,
    ListActionItemComponent,
    MoveToGiftListComponent,
    DeleteWlItemComponent,
    AddGiftListComponent,
    AddContactComponent
  ],
  imports: [
    NgbModule.forRoot(),
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    ListActionComponent,
    ListActionItemComponent,
    MoveToGiftListComponent,
    DeleteWlItemComponent,
    AddGiftListComponent,
    AddContactComponent
  ]
})
export class ListActionModule { }
