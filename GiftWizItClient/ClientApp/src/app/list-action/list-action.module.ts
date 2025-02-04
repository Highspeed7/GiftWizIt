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
import { ShareGiftListComponent } from './share-gift-list/share-gift-list.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { SharedGiftListService } from '../shared-gift-list/services/shared-gift-list.service';
import { DeleteGlItemComponent } from './delete-gl-item/delete-gl-item.component';
import { EditGiftListComponent } from './edit-gift-list/edit-gift-list.component';

@NgModule({
  declarations: [
    ListActionComponent,
    ListActionItemComponent,
    MoveToGiftListComponent,
    DeleteWlItemComponent,
    DeleteGlItemComponent,
    AddGiftListComponent,
    AddContactComponent,
    ShareGiftListComponent,
    DeleteGlItemComponent,
    EditGiftListComponent
  ],
  imports: [
    NgbModule.forRoot(),
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgMultiSelectDropDownModule.forRoot()
  ],
  exports: [
    ListActionComponent,
    ListActionItemComponent,
    MoveToGiftListComponent,
    DeleteWlItemComponent,
    DeleteGlItemComponent,
    AddGiftListComponent,
    AddContactComponent,
    ShareGiftListComponent,
    EditGiftListComponent
  ],
  providers: [
    SharedGiftListService
  ]
})
export class ListActionModule { }
