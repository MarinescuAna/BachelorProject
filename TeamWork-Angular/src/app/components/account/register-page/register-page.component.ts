import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserRegisterModule } from 'src/app/modules/user-register.module';
import { AuthService } from 'src/app/shared/auth.service';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit {
  private pattern =/^([^@\s]+)@((?:[-a-z0-9]+\.)+[a-z]{2,})$/i;
  formRegister = new FormGroup({
    firstName: new FormControl('',[Validators.required]),
    lastName: new FormControl('',[Validators.required]),
    institution: new FormControl('', Validators.required),
    email: new FormControl('',[Validators.required,Validators.pattern(this.pattern)]),
    role: new FormControl('',Validators.required),
    password: new FormControl('',[Validators.required,Validators.minLength(6)]),
    passwordConfirmation: new FormControl('',[Validators.required,Validators.minLength(6)])
  });
  hide: Boolean = true;
  hide2: Boolean = true;
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
  }

  onSubmit(): void{
    let userRegister=new UserRegisterModule();
    userRegister.firstName=this.formRegister.value.firstName;
    userRegister.lastName=this.formRegister.value.lastName;
    userRegister.institution=this.formRegister.value.institution;
    userRegister.emailAddress=this.formRegister.value.email;
    userRegister.password=this.formRegister.value.password;
    userRegister.userRole=this.formRegister.value.role;
    this.authService.register(userRegister);
  }
}
