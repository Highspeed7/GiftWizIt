import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactsComponent } from './contacts.component';
import { RouterModule } from '@angular/router';
import { MsalGuard, MsalService } from '@azure/msal-angular';
import { ListActionModule } from '../list-action/list-action.module';
import { ContactService } from './contact.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    ContactsComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    ListActionModule,
    RouterModule.forRoot([
      {
        path: "contacts",
        component: ContactsComponent,
        canActivate: [MsalGuard]
      }
    ])
  ],
  providers: [ContactService, MsalService]
})
export class ContactsModule { }
