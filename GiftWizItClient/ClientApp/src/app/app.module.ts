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
