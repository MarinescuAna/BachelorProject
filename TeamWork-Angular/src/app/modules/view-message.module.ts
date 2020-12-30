import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})

export class ViewMessageModule{
    messageKey:string;
    userName:string;
    content:string;
    dateSent:string;
}