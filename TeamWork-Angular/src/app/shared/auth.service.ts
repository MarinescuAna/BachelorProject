import { Injectable, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { TokenModule } from '../modules/token.module';
import { UserLoginModule } from '../modules/user-login.module';
import { DataService } from '../services/data.service';
import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends DataService {
  constructor(injector: Injector,private route: Router,private jwtDecode: JwtHelperService) {
    super(injector, 'Account');
  }

  private setLocalStorage(token: TokenModule): void {
    localStorage.setItem('access_token', token.accessToken);
    localStorage.setItem('refresh_token', token.refershToken);
  }

  public decodeJWToken(tag:string):string{
    const decode=this.jwtDecode.decodeToken(localStorage.getItem('access_token'));
    return decode==null? "":decode[tag];
  }
  
  public decodeJWRefreshToken(tag:string):string{
    const decode=this.jwtDecode.decodeToken(localStorage.getItem('refresh_token'));
    return decode==null? "":decode[tag];
  }

  private removeLocalStorage(): void {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
  }

  getToken(): any {
    if (localStorage.getItem('access_token') !== null) {
      let token: TokenModule;
      token.accessToken = localStorage.getItem('access_token');
      token.refershToken = localStorage.getItem('refresh_token');
      return token;
    }
    return null;
  }

  isLogged(): boolean{
    return localStorage.getItem('is_logged')==='true';
  }

  doLogout(): void {
    debugger
    this.removeLocalStorage();
    localStorage.setItem('is_logged', 'false');
    this.route.navigateByUrl('/landing-page');
  }

  register(user: UserLoginModule): void {
    super.post<TokenModule>('Register', user).subscribe(cr => {
      this.setLocalStorage(cr as TokenModule);
      localStorage.setItem('is_logged', 'true');
      this.route.navigateByUrl('/landing-page');
    });
  }

  login(user: UserLoginModule): void {
    debugger
    super.post<TokenModule>('Login', user).subscribe(cr => {
      this.setLocalStorage(cr as TokenModule);
      localStorage.setItem('is_logged', 'true');
      this.route.navigateByUrl('/landing-page');
    });
  }

  GetUserInfo(): any {
    return super.getMany('GetUserInfo');
  }
}
