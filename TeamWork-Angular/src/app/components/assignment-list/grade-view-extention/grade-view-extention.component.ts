import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import {DisplayGradesModule} from 'src/app/modules/display-grades.module';
import { GradeService } from 'src/app/services/grades.service';
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

  constructor(private gradesService: GradeService, private dialog: MatDialog) {
   }

  onExpandGrades() {
    this.panelOpenStateGrades = true;
    if (this.dataSource == null || this.dataSource.length <= 0) {
        this.gradesService.GetGrade(this.key).subscribe(cr => {
        this.dataSource = new MatTableDataSource<DisplayGradesModule>(cr as DisplayGradesModule[]);
        this.dataSource.paginator=this.paginator;
      });
    }
  }

  onStudentAverage(){
    const diagRef = this.dialog.open(AveragePercentDialogComponent, { width: '50%', data: { grade: this.dataSource.data  as DisplayGradesModule[] } });
  }
}
