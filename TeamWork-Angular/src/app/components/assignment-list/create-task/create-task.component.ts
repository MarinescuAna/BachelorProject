import { Component, Inject, Injector, OnInit, Optional } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import {CreateTaskModule} from 'src/app/modules/create-task.module';
import { ListDisplayModule } from 'src/app/modules/list-display.module';
import { AssignmentService } from 'src/app/services/assignment.service';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.css']
})
export class CreateTaskComponent implements OnInit {

  formlist = new FormGroup({
    title: new FormControl('',[Validators.required]),
    description: new FormControl('',[Validators.required]),
    deadline: new FormControl('',[]),
    max: new FormControl('',[]),
    chkdeadline: new FormControl('',[]),
  });

  assignment:ListDisplayModule;
  noDeadline=false;
  constructor(private injector: Injector,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any, private service: AssignmentService) {
    this.assignment=this.data.data as ListDisplayModule;
    this.noDeadline=this.assignment.deadline!=''? false:true;
   }

  ngOnInit(): void {
  }

  OnSubmit(){
    let task=new CreateTaskModule();
    task.description=this.formlist.value.description;
    task.title=this.formlist.value.title;
    task.checklistDeadline=this.formlist.value.chkdeadline;
    task.deadline=this.noDeadline==false? this.assignment.deadline:this.formlist.value.deadline;
    task.groupsMax=this.formlist.value.max;
    task.listId=this.assignment.key;
    this.service.CreateTask(task).subscribe(cr=>{
    this.service.alertService.showSucces("The assignment was created!");
  });
}
}
