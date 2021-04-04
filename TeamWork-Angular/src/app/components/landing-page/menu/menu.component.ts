import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/shared/auth.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {

  email: string;
  isTeacher=false;

  constructor(public authService: AuthService, private route: Router) {
    this.email = this.authService.decodeJWRefreshToken('unique_name');
    this.isTeacher=this.authService.decodeJWToken("role")==="STUDENT"?false:true;
  }

  onSubmit(): void {
    this.authService.doLogout();
  }

  redirectTo(url: string): void {
    this.route.navigateByUrl(url);
  }

  redirectHome() {
      this.route.navigateByUrl('landing-page')
  }
}