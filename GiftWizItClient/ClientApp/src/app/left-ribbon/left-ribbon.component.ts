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

  public ribbonItemClicked(e: any) {
    e.preventDefault();
    let element: HTMLElement = e.currentTarget;
    let anchor: any = element.firstElementChild;
    anchor.click();
  }
}
