import { Component, OnInit, NgZone } from '@angular/core';
import { WindowRefService } from '../window-ref.service';

@Component({
  selector: 'gw-sell-up-home',
  templateUrl: './sell-up-home.component.html',
  styleUrls: ['./sell-up-home.component.css']
})
export class SellUpHomeComponent implements OnInit {

  private window;

  constructor(
    private windowRef: WindowRefService
  ) {
    this.window = this.windowRef.nativeWindow;
  }

  ngOnInit() {
    
  }
}
