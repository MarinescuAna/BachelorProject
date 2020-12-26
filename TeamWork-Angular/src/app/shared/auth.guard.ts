import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import{AuthService} from './auth.service';


@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {

  constructor(public authService: AuthService,
    private router: Router){}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      debugger
      if(next.routeConfig.data?.roles!=null && next.routeConfig.data.roles[0].toLowerCase()==this.authService.decodeJWToken('role').toLowerCase()){
        return true;
      }
      if(this.authService.isLogged() !== true){
        window.alert("Access not allowed!");
        this.router.navigateByUrl('/login');
      }
      return true;
  }
  
}
