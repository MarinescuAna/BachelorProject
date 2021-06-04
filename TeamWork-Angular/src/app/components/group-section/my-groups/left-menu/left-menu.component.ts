import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/shared/auth.service';

@Component({
  selector: 'app-left-menu',
  templateUrl: './left-menu.component.html',
  styleUrls: ['./left-menu.component.css']
})
export class LeftMenuComponent implements OnInit {

  isTeacher=false;
  constructor( private router: Router,
    private authService: AuthService ) {
      this.isTeacher = this.authService.decodeJWToken("role") === "STUDENT" ? false : true;
  }

  ngOnInit(): void {
  }
  redirectTo(url: string): void {
    this.router.navigateByUrl(url);
  }

}
