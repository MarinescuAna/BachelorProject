
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})

export class TokenModule {
  accessToken: string;
  accessTokenExpiration: string;
  refershToken: string;
  refershTokenExpiration: string;
}
