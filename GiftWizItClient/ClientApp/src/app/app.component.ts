import { Component } from '@angular/core';
import { WindowRefService } from './window-ref.service';
import { AccountsService } from './accounts.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { map } from 'rxjs/operators';
import { URLSearchParams } from '@angular/http';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public title = 'app';
  public displayName: string = "Guest";
  public isLoggedIn: boolean = false;
  private window: any;

  constructor(private windowRef: WindowRefService, private acntSvc: AccountsService, private router: Router, private route: ActivatedRoute) {
    this.window = this.windowRef.nativeWindow;
    this.acntSvc.logingedIn$.subscribe((l: boolean) => {
      this.isLoggedIn = l;
      if (this.window.gw_app !== undefined && this.window.gw_app.userInfo !== undefined && this.window.gw_app.userInfo.name !== undefined) {
        this.displayName = this.window.gw_app.userInfo.name;
        this.isLoggedIn = true;
      } else {
        this.displayName = "Guest";
      }
    });
  }



  //public onLogout() {
  //    this.acntSvc.logout().subscribe((r: any) => {
  //        if (r.status === 200) {
  //            this.acntSvc.loggedInSrc.next(false);
  //            console.log(r);
  //            this.isLoggedIn = false;
  //            localStorage.removeItem("access_token");
  //            this.router.navigate(['']);
  //            this.username = "Guest";
  //        }
  //    });
  //}
}
