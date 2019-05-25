import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactGetStartedComponent } from './contact-get-started.component';
import { ContactService } from '../contacts/contact.service';

@NgModule({
  declarations: [
    ContactGetStartedComponent
  ],
  imports: [
    CommonModule
  ],
  providers: [ContactService]
})
export class ContactGetStartedModule { }
