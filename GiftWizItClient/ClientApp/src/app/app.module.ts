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
import { GiftTagComponent } from './gift-tag/gift-tag.component';

@NgModule({
  declarations: [
    AppComponent,
    LeftRibbonComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    ReactiveFormsModule,
    CommonModule,
    AuthenticationModule,
    GiftListsModule,
    WishListModule,
    RouterModule.forRoot([
      {
        path: "login",
        component: LoginComponent
      }
    ])
  ],
  providers: [AccountsService, MsalService, AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
