import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})

export class UserRegisterModule {
    firstName: string;
    lastName: string;
    password: string;
    emailAddress: string;
    institution: string;
    userRole: string;
  }
  