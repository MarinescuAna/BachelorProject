import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})

export class CreateTaskModule{
      listId :string;
      title:string;
      description:string;
      deadline:string;
      checklistDeadline:string;
      groupsMax: string; 

}