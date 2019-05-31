import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ContactService } from '../contacts/contact.service';
import { Utilities } from '../utils/Utilities';

@Component({
  selector: 'gw-contact-get-started',
  templateUrl: './contact-get-started.component.html',
  styleUrls: ['./contact-get-started.component.css']
})
export class ContactGetStartedComponent implements OnInit {

  private utilities: Utilities;

  constructor(
    private route: ActivatedRoute,
    private contactSvc: ContactService,
    private router: Router
  ) {
    this.utilities = new Utilities();
  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      if (!this.utilities.isEmpty(params)) {
        this.contactSvc.verifyEmail(params["emailId"]).then(res => { alert("Email verified; thanks!") });
        this.router.navigate(["contact-get-started"]);
      }
    });
  }
}
