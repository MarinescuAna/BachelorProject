import { Injectable, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { TokenModule } from '../modules/token.module';
import { UserLoginModule } from '../modules/user-login.module';
import { DataService } from '../services/data.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends DataService {
  constructor(injector: Injector,private route: Router) {
    super(injector, 'Account');
  }

  private setLocalStorage(token: TokenModule): void {
    localStorage.setItem('access_token', token.accessToken);
    localStorage.setItem('access_token_expiration', token.accessTokenExpiration);
    localStorage.setItem('refresh_token', token.refershToken);
    localStorage.setItem('refresh_token_expiration', token.refershTokenExpiration);
  }

  private setUser(userEmail: string): void {
    localStorage.setItem('email', userEmail);
  }

  public getUserEmail(): string {
    return localStorage.getItem('email');
  }

  private removeLocalStorage(): void {
    localStorage.removeItem('access_token');
    localStorage.removeItem('access_token_expiration');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('refresh_token_expiration');
  }

  getToken(): any {
    if (localStorage.getItem('access_token') !== null) {
      let token: TokenModule;
      token.accessToken = localStorage.getItem('access_token');
      token.accessTokenExpiration = localStorage.getItem('access_token_expiration');
      token.refershToken = localStorage.getItem('refresh_token');
      token.refershTokenExpiration = localStorage.getItem('refresh_token_expiration');
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
      this.setUser(user.emailAddress);
      this.setLocalStorage(cr as TokenModule);
      localStorage.setItem('is_logged', 'true');
      this.route.navigateByUrl('/create-group');
    });
  }

  login(user: UserLoginModule): void {
    debugger
    super.post<TokenModule>('Login', user).subscribe(cr => {
      this.setUser(user.emailAddress);
      this.setLocalStorage(cr as TokenModule);
      localStorage.setItem('is_logged', 'true');
      this.route.navigateByUrl('/create-group');
    });
  }

}
