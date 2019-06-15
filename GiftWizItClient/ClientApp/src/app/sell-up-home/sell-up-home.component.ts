import { Component, OnInit, NgZone } from '@angular/core';
import { WindowRefService } from '../window-ref.service';

@Component({
  selector: 'gw-sell-up-home',
  templateUrl: './sell-up-home.component.html',
  styleUrls: ['./sell-up-home.component.css']
})
export class SellUpHomeComponent implements OnInit {

  private window;
  private topSectionScrollElement;

  constructor(
    private ngZone: NgZone,
    private windowRef: WindowRefService
  ) {
    this.window = this.windowRef.nativeWindow;
  }

  ngOnInit() {
    // Set the initial top
    this.topSectionScrollElement = this.window.document.querySelector("div.section1");
    //this.topSectionScrollElement.style.left = "-50vw";
    this.ngZone.runOutsideAngular(() => {
      this.window.addEventListener('wheel', this.scroll, true);
      this.window.addEventListener('touch', this.scroll, true);
    })
  }

  scroll = (event: any): void => {
    //const wheelVal = event.srcElement.
    const number = event.srcElement.scrollTop;
    console.log("scrolled!");
  }
}
