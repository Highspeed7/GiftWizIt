import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ContactService } from '../contacts/contact.service';

@Component({
  selector: 'gw-contact-get-started',
  templateUrl: './contact-get-started.component.html',
  styleUrls: ['./contact-get-started.component.css']
})
export class ContactGetStartedComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private contactSvc: ContactService,
  ) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.contactSvc.verifyEmail(params["emailId"]).then(res => { alert("Email verified; thanks!") });
    });
  }

}
