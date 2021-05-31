import { Component, Injector, Input, OnInit, ViewChild } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AssignedTasksPerGroupModule } from 'src/app/modules/assigned-tasks-per-group.module';
import { ListDisplayModule } from 'src/app/modules/list-display.module';
import { ListService } from 'src/app/services/list.service';
import { SheetKeyComponent } from '../../group-section/my-groups/sheet-key/sheet-key.component';
import { CreateTaskComponent } from '../create-task/create-task.component';
import { AssignmentsListModule } from 'src/app/modules/assigments-list.module';
import { AssignmentService } from 'src/app/services/assignment.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { AssignedTaskService } from 'src/app/services/assigned-task.service';
import { RedirectSolutionDialogComponent } from '../../group-section/redirect-solution-dialog/redirect-solution-dialog.component';
import { UpdateAssignedTaskComponent } from '../../group-section/update-assigned-task/update-assigned-task.component';
import { MainCheckDialogComponent } from '../../checklist-section/main-check-dialog/main-check-dialog.component';
import { CheckService } from 'src/app/services/check.service';
import { ChecklistGradeService } from 'src/app/services/checklist-grade.service';


@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  panelOpenState = false;
  panelOpenStateGroup = false;

  dataSource: any;
  dataSourceGroups: any;

  @Input() list: ListDisplayModule;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatPaginator) paginatorGroups: MatPaginator;

  displayedColumns: string[] = ['title', 'description', 'groupsMax', 'groupsTake', 'status', 'deadline', 'checklistDeadline', 'createdDate', 'gradechk', 'symbol'];
  displayedColumnsGroup: string[] = ['name', 'title', 'status', 'deadline', 'checklistDeadline', 'solution', 'grade', 'addgrade', 'symbol'];

  constructor(private _bottomSheet: MatBottomSheet,
    private assignmentService: AssignmentService,
    private assignTaskService: AssignedTaskService,
    private checklistGradeService: ChecklistGradeService,
    private dialog: MatDialog,
    public listService: ListService) {
  }

  ngOnInit(): void {
  }

  onGoTo(link: any) {
    const diagRef = this.dialog.open(RedirectSolutionDialogComponent, { width: '40%', data: { data: link } });
  }

  onUpdate(id: any) {
    const diagRef = this.dialog.open(UpdateAssignedTaskComponent, { data: { data: id } });
    this.dataSourceGroups = null;
  }

  openKeySheet(): void {
    this._bottomSheet.open(SheetKeyComponent, { data: { key: this.list.key } });
  }

  onExpand() {
    this.panelOpenState = true
    if (this.dataSource == null || this.dataSource.length <= 0) {
      this.assignmentService.GetAssignments(this.list.key).subscribe(cr => {
        this.dataSource = new MatTableDataSource<AssignmentsListModule>(cr as AssignmentsListModule[]);
        this.dataSource.paginator = this.paginator;
      });
    }
  }

  onCreateTask() {
    const diagRef = this.dialog.open(CreateTaskComponent, { width: '40%', data: { data: this.list } });
    this.dataSource = null;
  }

  onExpandGroupSection() {
    this.panelOpenStateGroup = true;
    if (this.dataSourceGroups == null || this.dataSourceGroups.length <= 0) {
      this.assignTaskService.GetTasksPerGroup(this.list.key).subscribe(cr => {
        this.dataSourceGroups = new MatTableDataSource<AssignedTasksPerGroupModule>(cr as AssignedTasksPerGroupModule[]);
        this.dataSourceGroups.paginator = this.paginatorGroups;
      });
    }
  }

  onDelete(id: any): void {
    if (confirm("Are you sure?")) {
      this.assignmentService.DeleteAssignment(id).subscribe(cr => {
        this.assignmentService.alertService.showSucces('The assignment was removed!');
        window.location.reload();
      });
    }

  }

  onCheckList(id: any) {
    const diagRef = this.dialog.open(MainCheckDialogComponent, { width: '60%', height: '80%', data: { data: id } });
  }

  onDeleteList(): void {
    if (confirm("Are you sure?")) {
      this.listService.DeleteList(this.list.key).subscribe(cr => {
        this.assignmentService.alertService.showSucces('The list was removed!');
        window.location.reload();
      });
    }

  }

  onReturnChecklistGrades(id: any) {
    if (confirm("Are you sure you want to return the notes? You can only do this once per assignment?")) {
      this.checklistGradeService.ReturnCheckListGrade(id).subscribe(cr => {
        this.assignmentService.MarkAsReturnChecklistGrades(id).subscribe(cr => {
          this.checklistGradeService.alertService.showSucces("The grades was returned!");
          window.location.reload();
        });
      });
    }
  }
}
