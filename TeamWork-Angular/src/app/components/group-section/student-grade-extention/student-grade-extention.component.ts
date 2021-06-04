import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { DisplayGradesModule } from 'src/app/modules/display-grades.module';
import { ListDisplayModule } from 'src/app/modules/list-display.module';
import { GradeService } from 'src/app/services/grades.service';

@Component({
  selector: 'app-student-grade-extention',
  templateUrl: './student-grade-extention.component.html',
  styleUrls: ['./student-grade-extention.component.css']
})
export class StudentGradeExtentionComponent  {

  panelOpenStateGrades = false;
  @Input() list: ListDisplayModule;
  dataSource: any;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  displayedColumns: string[] = ['assignment', 'checklist', 'teacher', 'peer', 'average','comment'];

  constructor(private gradesService: GradeService) {
   }

  onExpandGrades() {
    this.panelOpenStateGrades = true;
    if (this.dataSource == null || this.dataSource.length <= 0) {
        this.gradesService.GetCurrentUserGrades(this.list.key).subscribe(cr => {
        this.dataSource = new MatTableDataSource<DisplayGradesModule>(cr as DisplayGradesModule[]);
        this.dataSource.paginator=this.paginator;
      });
    }
  }


}
