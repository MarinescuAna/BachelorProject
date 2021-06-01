import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { AssignmentsListModule } from 'src/app/modules/assigments-list.module';
import { ListDisplayModule } from 'src/app/modules/list-display.module';
import { AssignmentService } from 'src/app/services/assignment.service';
import { ChecklistGradeService } from 'src/app/services/checklist-grade.service';
import { ListService } from 'src/app/services/list.service';
import { SheetKeyComponent } from '../../group-section/my-groups/sheet-key/sheet-key.component';
import { CreateTaskComponent } from '../create-task/create-task.component';

@Component({
  selector: 'app-list-view-extention',
  templateUrl: './list-view-extention.component.html',
  styleUrls: ['./list-view-extention.component.css']
})
export class ListViewExtentionComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  panelOpenState = false;
  dataSource: any;
  @Input() list: ListDisplayModule;
  displayedColumns: string[] = ['title', 'description', 'groupsMax', 'groupsTake', 'status', 'deadline', 'checklistDeadline', 'createdDate', 'gradechk', 'symbol'];
 
  constructor(
    private _bottomSheet: MatBottomSheet,
    private dialog: MatDialog,
    private assignmentService: AssignmentService,
    private checklistGradeService: ChecklistGradeService,
    private listService:ListService
  ) { }

  ngOnInit(): void {
  }

  onCreateTask() {
    const diagRef = this.dialog.open(CreateTaskComponent, { width: '40%', data: { data: this.list } });
    this.dataSource = null;
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
  openKeySheet(): void {
    this._bottomSheet.open(SheetKeyComponent, { data: { key: this.list.key } });
  }
  onDelete(id: any): void {
    if (confirm("Are you sure?")) {
      this.assignmentService.DeleteAssignment(id).subscribe(cr => {
        this.assignmentService.alertService.showSucces('The assignment was removed!');
        window.location.reload();
      });
    }

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
