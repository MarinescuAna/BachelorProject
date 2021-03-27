import { Component, Injector, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/services/alert.service';
import { AuthService } from 'src/app/shared/auth.service';

@Component({
  selector: 'app-contact-page',
  templateUrl: './contact-page.component.html',
  styleUrls: ['./contact-page.component.css']
})
export class ContactPageComponent implements OnInit {

  constructor(private injector: Injector,private route: Router,public authService: AuthService) { }

  ngOnInit(): void {
  }
  onSubmit():void{

    this.injector.get(AlertService).showSucces('The message was sent!');
    if (this.authService.isLogged()) {
      this.route.navigateByUrl('home-logged')
    }
    else {
      this.route.navigateByUrl('landing-page')
    }
  }
}
