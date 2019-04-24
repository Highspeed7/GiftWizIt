import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-left-ribbon',
  templateUrl: './left-ribbon.component.html',
  styleUrls: ['./left-ribbon.component.css']
})
export class LeftRibbonComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  public ribbonItemClicked(e: Event) {
    e.stopPropagation();
    let event = new MouseEvent("click");
    var element: Element = e.srcElement;
    element.firstChild.dispatchEvent(event);
  }
}
