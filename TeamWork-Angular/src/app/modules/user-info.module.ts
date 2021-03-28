import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})

export class UserInfoModule {
  firstName: string;
  lastName: string;
  email: string;
  institution: string;
  role: string;
  imageContent: string;
  imageExtention: string;
}