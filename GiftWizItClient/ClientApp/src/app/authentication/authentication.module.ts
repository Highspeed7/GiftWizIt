import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MsalModule, MsalService } from '@azure/msal-angular';
import { HttpClientModule } from '@angular/common/http'
import * as env from '../../environments/environment';
import { protectedResourceMap } from '../configs/protectedResource';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule,
    MsalModule.forRoot({
      clientID: "aa2f99ef-10ae-4219-ae07-224d88203152",
      authority: "https://login.microsoftonline.com/tfp/giftwizit.onmicrosoft.com/B2C_1_SigninSignup1",
      consentScopes: ["https://giftwizit.onmicrosoft.com/api/read", "https://giftwizit.onmicrosoft.com/api/user_impersonation"],
      redirectUri: env.environment.redirectUri,
      postLogoutRedirectUri: env.environment.redirectUri,
      cacheLocation: "localStorage"
    })
  ],
  providers: [MsalService]
})
export class AuthenticationModule { }
