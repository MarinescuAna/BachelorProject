import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { ViewMembersModule } from 'src/app/modules/view-members.module';
import { AuthService } from 'src/app/shared/auth.service';
import { CheckDisplayModule } from 'src/app/modules/check-display.module';
import { CheckService } from 'src/app/services/check.service';
import { MatTableDataSource } from '@angular/material/table';
import { InsertCheckModule } from 'src/app/modules/insert-check.module';
import { CreateCheckDialogComponent } from '../create-check-dialog/create-check-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import{UpdateCheckModule} from 'src/app/modules/update-check.module';
import { UpdateCheckDialogComponent } from '../update-check-dialog/update-check-dialog.component';

@Component({
  selector: 'app-student-check-section',
  templateUrl: './student-check-section.component.html',
  styleUrls: ['./student-check-section.component.css']
})

export class StudentCheckSectionComponent implements OnInit {

  panelOpenState = false;
  dataSource: any;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  displayedColumns: string[] = ['checked', 'description','update', 'creationDate', 'symbol'];
  isTeacher = false;
  @Input() student: ViewMembersModule;
  @Input() assignedTaskId: string;

  constructor(private authService: AuthService, public checkService: CheckService, private dialog: MatDialog) {
    this.isTeacher = this.authService.decodeJWToken("role") === "STUDENT" ? false : true;
  }

  ngOnInit(): void {
  }

  onExpand() {
    this.panelOpenState = true
    if (this.dataSource == null || this.dataSource.length <= 0) {
      let data = this.assignedTaskId + '*' + this.student.email;
      this.checkService.GetChecks(data).subscribe(cr => {
        this.dataSource = new MatTableDataSource<CheckDisplayModule>(cr as CheckDisplayModule[]);
        this.dataSource.paginator = this.paginator;
      });
    }
  }
  onCreateCheck(){
    let newCheck=new InsertCheckModule();
    newCheck.email=this.student.email;
    newCheck.assignedTaskId=this.assignedTaskId;
    const diagRef = this.dialog.open(CreateCheckDialogComponent, {width: '40%', data: { data: newCheck } });
    this.dataSource=null;
  }

  onUpdate(element:any){
    debugger
    let newCheck=new UpdateCheckModule();
    newCheck.checkID=element.checkID;
    newCheck.description=element.description;
    const diagRef = this.dialog.open(UpdateCheckDialogComponent, {width: '40%', data: { data: newCheck } });
    this.dataSource=null;
  }

  onDelete(id:any){
    this.checkService.DeleteCheck(id).subscribe(cr => {
      this.checkService.alertService.showSucces("The item was deleted!");
      this.dataSource=null;
    });
  }

}
