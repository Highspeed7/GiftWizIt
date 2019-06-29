import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { Observable } from 'rxjs';

@Injectable()
export class HomeGuard implements CanActivate {
  constructor(
    private msal: MsalService,
    private router: Router
  ) { }

  canActivate(route: ActivatedRouteSnapshot, router: RouterStateSnapshot): boolean | Promise<boolean> | Observable<boolean> {
    var user = this.msal.getUser();

    if (user !== null) {
      return true;
    } else {
      this.router.navigate(["welcome"]);
    }
  }
}
