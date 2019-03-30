import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { LeftRibbonComponent } from './left-ribbon/left-ribbon.component';
import { RegisterComponent } from './register/register.component';
import { HttpModule } from '@angular/http';

// Services
import { AccountsService } from './accounts.service';
import { LoginComponent } from './login/login.component';
import { CommonModule } from '@angular/common';
import { AuthenticationModule } from './authentication/authentication.module';

@NgModule({
  declarations: [
    AppComponent,
    LeftRibbonComponent,
    RegisterComponent,
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
        path: "register",
        component: RegisterComponent
      },
      {
        path: "login",
        component: LoginComponent
      }
    ])
  ],
  providers: [AccountsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
