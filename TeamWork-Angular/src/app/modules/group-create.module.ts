
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})

export class GroupCreateModule {
    groupName: string;
    description: string;
    teacherEmail: string;
    studentCreatorEmail: string;
}
