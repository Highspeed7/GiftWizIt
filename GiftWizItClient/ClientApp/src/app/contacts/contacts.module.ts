import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactsComponent } from './contacts.component';
import { RouterModule } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { ListActionModule } from '../list-action/list-action.module';

@NgModule({
  declarations: [
    ContactsComponent
  ],
  imports: [
    CommonModule,
    ListActionModule,
    RouterModule.forRoot([
      {
        path: "contacts",
        component: ContactsComponent,
        canActivate: [MsalGuard]
      }
    ])
  ]
})
export class ContactsModule { }
