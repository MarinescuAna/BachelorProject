import { Component } from '@angular/core';
import { AuthService } from 'src/app/shared/auth.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {

  email:string;
  constructor(public authService: AuthService){
    this.email=localStorage.getItem('email');
  }
  onSubmit(): void{
    this.authService.doLogout();
  }
}