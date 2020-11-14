import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/shared/auth.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {

  @ViewChild('clickMe') clickMe: any;
  email:string;

  constructor(public authService: AuthService,private route: Router){
    this.email=localStorage.getItem('email');
  }

  onSubmit(): void{
    this.authService.doLogout();
  }

  clickOnHover() {
    this.clickMe._elementRef.nativeElement.click();
  }

  redirectTo(url:string):void{
    this.route.navigateByUrl(url);
  }
}