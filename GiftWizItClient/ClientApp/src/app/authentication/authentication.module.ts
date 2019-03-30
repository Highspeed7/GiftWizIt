import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MsalModule, MsalService } from '@azure/msal-angular';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MsalModule.forRoot({
      clientID: "aa2f99ef-10ae-4219-ae07-224d88203152",
      consentScopes: ["user_impersonation"],
      authority: "https://login.microsoftonline.com/tfp/giftwizit.onmicrosoft.com/B2C_1_SigninSignup1"
    })
  ],
  providers: [MsalService]
})
export class AuthenticationModule { }
