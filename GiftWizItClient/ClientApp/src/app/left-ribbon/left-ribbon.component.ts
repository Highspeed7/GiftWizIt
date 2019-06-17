import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../authentication/services/auth.service';

@Component({
  selector: 'app-left-ribbon',
  templateUrl: './left-ribbon.component.html',
  styleUrls: ['./left-ribbon.component.css']
})
export class LeftRibbonComponent implements OnInit {

  @Input()
  public isAuthenticated = false;

  constructor(private authSvc: AuthService) { }

  ngOnInit() {
  }

  public ribbonItemClicked(e: any) {
    e.preventDefault();
    let element: HTMLElement = e.currentTarget;
    let anchor: any = element.firstElementChild;
    anchor.click();
  }
}
