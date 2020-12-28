
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})

export class GroupUpdateModule {
    groupName: string;
    description: string;
    id:string;
    oldGroupName:string;
}
