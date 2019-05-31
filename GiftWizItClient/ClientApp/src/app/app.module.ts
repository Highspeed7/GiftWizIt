import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { LeftRibbonComponent } from './left-ribbon/left-ribbon.component';
import { HttpModule } from '@angular/http';

// Services
import { AccountsService } from './accounts.service';
import { LoginComponent } from './login/login.component';
import { CommonModule } from '@angular/common';
import { AuthenticationModule } from './authentication/authentication.module';
import { MsalService } from '@azure/msal-angular';
import { AuthService } from './authentication/services/auth.service';
import { GiftListsModule } from './gift-list/gift-lists.module';
import { WishListModule } from './wish-list/wish-list.module';
import { ContactsModule } from './contacts/contacts.module';
import { ContactGetStartedComponent } from './contact-get-started/contact-get-started.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { SharedGiftListModule } from './shared-gift-list/shared-gift-list.module';
import { DialogModule } from './dialog/dialog.module';
import { SharedListAccessModalComponent } from './shared-gift-list/shared-list-access-modal/shared-list-access-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    LeftRibbonComponent,
    LoginComponent,
    ContactGetStartedComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    ReactiveFormsModule,
    CommonModule,
    DialogModule,
    NgMultiSelectDropDownModule.forRoot(),
    AuthenticationModule,
    GiftListsModule,
    WishListModule,
    ContactsModule,
    SharedGiftListModule,
    RouterModule.forRoot([
      {
        path: "login",
        component: LoginComponent
      },
      {
        path: "contact-get-started",
        component: ContactGetStartedComponent
      }
    ])
  ],
  providers: [AccountsService, MsalService, AuthService],
  bootstrap: [AppComponent],
  entryComponents: [SharedListAccessModalComponent]
})
export class AppModule { }
