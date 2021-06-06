import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { AssignmentsListModule } from 'src/app/modules/assigments-list.module';
import {DisplayGradesModule} from 'src/app/modules/display-grades.module';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';
import { AssignmentService } from 'src/app/services/assignment.service';
import { GradeService } from 'src/app/services/grades.service';
import { GroupService } from 'src/app/services/group-service';
import { AveragePercentDialogComponent } from '../average-percent-dialog/average-percent-dialog.component';

@Component({
  selector: 'app-grade-view-extention',
  templateUrl: './grade-view-extention.component.html',
  styleUrls: ['./grade-view-extention.component.css']
})
export class GradeViewExtentionComponent {

  panelOpenStateGrades = false;
  @Input() key: string;
  dataSource: any;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  displayedColumns: string[] = ['name', 'group', 'assignment', 'checklist', 'teacher', 'peer', 'average','comment'];  
  form = new FormGroup({
    group: new FormControl('', [Validators.required]),
    assignment: new FormControl('', [Validators.required])
  });
  assignments: AssignmentsListModule[];
  groups: ViewGroupsModule[];
  gradesOrigin:any;
  constructor(
    private groupService: GroupService,
    private assignmentService: AssignmentService,
    private gradesService: GradeService, 
    private dialog: MatDialog) {
   }

  onExpandGrades() {
    this.panelOpenStateGrades = true;
    if (this.dataSource == null || this.dataSource.length <= 0) {
        this.gradesService.GetGrade(this.key).subscribe(cr => {
        this.gradesOrigin=cr as DisplayGradesModule[];
        this.dataSource = new MatTableDataSource<DisplayGradesModule>(this.gradesOrigin);
        this.dataSource.paginator=this.paginator;
      });
    }
    if (this.groups == null || this.groups.length == 0) {
      this.groupService.GetListGroups(this.key).subscribe(cr => {
        this.groups = cr as ViewGroupsModule[];
      });
    }

    if (this.assignments == null || this.assignments.length == 0) {
      this.assignmentService.GetAssignments(this.key).subscribe(cr => {
        this.assignments = cr as AssignmentsListModule[];
      });
    }
  }
  onSearch() {
    let copyGrades=this.gradesOrigin;
    if (this.form.value.group != "") {
      console.log("if 1 with: "+this.form.value.group );  
      copyGrades = copyGrades.filter(element => element.groupName == this.form.value.group)
    }
    if (this.form.value.assignment != "") {
      console.log("if 2 with: "+this.form.value.assignment );      
      copyGrades = copyGrades.filter(element => element.assignmentTitle === this.form.value.assignment)
    }

    this.dataSource = new MatTableDataSource<DisplayGradesModule>(copyGrades);
    this.dataSource.paginator=this.paginator;
  }
  onReload(){
    this.dataSource = new MatTableDataSource<DisplayGradesModule>(this.gradesOrigin);
    this.dataSource.paginator=this.paginator;
  }
  onStudentAverage(){
    const diagRef = this.dialog.open(AveragePercentDialogComponent, { width: '50%', data: { grade: this.dataSource.data  as DisplayGradesModule[] } });
  }
}
