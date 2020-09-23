import { Injectable, Injector } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { TokenModule } from '../modules/token.module';
import { UserLoginModule } from '../modules/user-login.module';
import { UserRegisterModule } from '../modules/user-register.module';
import { DataService } from '../services/data.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends DataService {

  private isLoggedInSubject: BehaviorSubject<boolean>;
  public isLoggedIn: Observable<boolean>;
  
  constructor(injector: Injector) {
    super(injector, 'Account');
    this.isLoggedInSubject = new BehaviorSubject<boolean>(localStorage.getItem('access_token') != null);
    this.isLoggedIn = this.isLoggedInSubject.asObservable();
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

  doLogout(): void {
    debugger
    this.removeLocalStorage();
    this.isLoggedInSubject.next(false);
    this.isLoggedIn = this.isLoggedInSubject.asObservable();
    //TODO: redirect to the guest page
  }

  register(user: UserLoginModule): void {
    super.post<TokenModule>('Register', user).subscribe(cr => {
      this.setUser(user.emailAddress);
      this.setLocalStorage(cr as TokenModule);
      this.isLoggedInSubject.next(true);
      this.isLoggedIn = this.isLoggedInSubject.asObservable();
      //TODO redirectare catre pagina de home a uritlizatorului
    });
  }

  login(user: UserLoginModule): void {
    debugger
    super.post<TokenModule>('Login', user).subscribe(cr => {
      this.setUser(user.emailAddress);
      this.setLocalStorage(cr as TokenModule);
      this.isLoggedInSubject.next(true);
      this.isLoggedIn = this.isLoggedInSubject.asObservable();
      //TODO redirectare catre pagina de home a uritlizatorului
    });
  }

}
