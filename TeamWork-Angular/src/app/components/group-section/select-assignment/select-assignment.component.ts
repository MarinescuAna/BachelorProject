import { Component, Inject, OnInit, Optional, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { AssignmentsListModule } from 'src/app/modules/assigments-list.module';
import { AssignmentService } from 'src/app/services/assignment.service';
import{AssignTaskModule}from'src/app/modules/assign-task.module';
import { AssignedTaskService } from 'src/app/services/assigned-task.service';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-select-assignment',
  templateUrl: './select-assignment.component.html',
  styleUrls: ['./select-assignment.component.css']
})
export class SelectAssignmentComponent implements OnInit {
  formJoinGroup = new FormGroup({
    key: new FormControl('',[Validators.required])
  });
  dataSource: any;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  displayedColumns: string[] = ['title', 'description', 'groupsMax', 'groupsTake', 'deadline','status', 'checklistDeadline', 'createdDate', 'symbol'];
  targhetList:string;

  constructor(public dialogRef: MatDialogRef<SelectAssignmentComponent>,@Optional() @Inject(MAT_DIALOG_DATA) public data: any,
  public assignmentService: AssignmentService,
  public assignService: AssignedTaskService) { 
    this.targhetList = data.data as string;
  }

  ngOnInit(): void {
  }

  onSelectTask(id:string){
    let task=new AssignTaskModule();
    task.listId=this.targhetList;
    task.assignmentId=id;
    this.assignService.AssignTask(task).subscribe(cr => {
      this.assignService.alertService.showSucces("The task was added!");
      this.dialogRef.close();
    });
  }

  onSubmit(){
    if (this.dataSource==null || this.dataSource.length == 0) {
      this.assignmentService.GetAssignments(this.formJoinGroup.value.key).subscribe(cr => {
        this.dataSource =new MatTableDataSource<AssignmentsListModule>(cr as AssignmentsListModule[]);
        this.dataSource.paginator = this.paginator;
      });
    }
  }
}
