import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import {DisplayGradesModule} from 'src/app/modules/display-grades.module';
import { GradeService } from 'src/app/services/grades.service';

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
  displayedColumns: string[] = ['name', 'group', 'assignment', 'checklist', 'teacher', 'peer', 'comment'];

  constructor(private gradesService: GradeService) {
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
}
