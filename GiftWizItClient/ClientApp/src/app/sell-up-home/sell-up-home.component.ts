import { Component, OnInit, NgZone } from '@angular/core';
import { WindowRefService } from '../window-ref.service';
import { MsalService } from '@azure/msal-angular';
import { Router } from '@angular/router';

@Component({
  selector: 'gw-sell-up-home',
  templateUrl: './sell-up-home.component.html',
  styleUrls: ['./sell-up-home.component.css']
})
export class SellUpHomeComponent implements OnInit {

  // TODO: Return to make linking more dynamic
  private chromeExtInstallUrl: "https://chrome.google.com/webstore/detail/ojbmfjenijdkdndemkbkongfaendkgic";
  private window;

  constructor(
    private windowRef: WindowRefService,
    private msal: MsalService,
    private router: Router
  ) {
    this.window = this.windowRef.nativeWindow;
  }

  ngOnInit() {
    var user = this.msal.getUser();
    if (user != null) {
      this.router.navigate([""]);
    }
  }

  public installExtension(url: string) {
    // TODO: Implement browser detection
    window.open(url, '_blank');
  }

}
