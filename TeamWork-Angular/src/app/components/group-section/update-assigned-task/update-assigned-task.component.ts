import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AssignedTaskService } from 'src/app/services/assigned-task.service';
import { AuthService } from 'src/app/shared/auth.service';
import {UpdateAssignedTaskModule} from 'src/app/modules/update-assigned-task.module';

@Component({
  selector: 'app-update-assigned-task',
  templateUrl: './update-assigned-task.component.html',
  styleUrls: ['./update-assigned-task.component.css']
})
export class UpdateAssignedTaskComponent implements OnInit {
  formUpdate = new FormGroup({
    grade: new FormControl('',[Validators.required]),
    solution: new FormControl('',[Validators.required]),
  });
  isTeacher=false;
  assignedTaskId:string;
  constructor(public dialogRef: MatDialogRef<UpdateAssignedTaskComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any, 
    private authService: AuthService,
    private assignedService: AssignedTaskService) {
      debugger
    this.assignedTaskId=data.data as string;
    this.isTeacher=this.authService.decodeJWToken("role")==="STUDENT"?false:true;
   }

  ngOnInit(): void {
  }
  onSubmit(): void{
    debugger
    let assignedTask=new UpdateAssignedTaskModule();
    if(this.isTeacher==true){
      assignedTask.teacherGrade=this.formUpdate.value.grade;
    }else{
      assignedTask.solutionLink=this.formUpdate.value.solution;
    }
    assignedTask.assignedTaskId=this.assignedTaskId;
    this.assignedService.UpdateAssignedTask(assignedTask).subscribe(cr => {
      this.assignedService.alertService.showSucces("The task was updated!")
      this.dialogRef.close();
    });
  }
}
