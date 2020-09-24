import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserLoginModule } from 'src/app/modules/user-login.module';
import { AuthService } from 'src/app/shared/auth.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})

export class LoginPageComponent implements OnInit {

  private pattern =/^([^@\s]+)@((?:[-a-z0-9]+\.)+[a-z]{2,})$/i;
  formLogin = new FormGroup({
    email: new FormControl('',[Validators.required,Validators.pattern(this.pattern)]),
    password: new FormControl('',[Validators.required,Validators.minLength(6)])
  });
  hide: Boolean = true;
  constructor(private authService: AuthService, private route: Router) { }

  ngOnInit(): void {
  }

  onSubmit(): void{
    let userLogin= new UserLoginModule();
    userLogin.emailAddress=this.formLogin.value.email;
    userLogin.password=this.formLogin.value.password;
    this.authService.login(userLogin);
  }

  onCreateAccount(): void{
    this.route.navigateByUrl('/register');
  }
}
