import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { ListDisplayModule } from 'src/app/modules/list-display.module';
import { AssignedTaskService } from 'src/app/services/assigned-task.service';
import{AssignedTaskModule} from 'src/app/modules/assigned-task-display.module';
import { SelectAssignmentComponent } from '../select-assignment/select-assignment.component';
import { UpdateAssignedTaskComponent } from '../update-assigned-task/update-assigned-task.component';
import { MatTableDataSource } from '@angular/material/table';
import { RedirectSolutionDialogComponent } from '../redirect-solution-dialog/redirect-solution-dialog.component';
import { AuthService } from 'src/app/shared/auth.service';
import { ListService } from 'src/app/services/list.service';
import { MainCheckDialogComponent } from '../../checklist-section/main-check-dialog/main-check-dialog.component';
@Component({
  selector: 'app-assignment',
  templateUrl: './assignment.component.html',
  styleUrls: ['./assignment.component.css']
})
export class AssignmentComponent implements OnInit {
  panelOpenState = false;
  dataSource: any;
  @Input() list: ListDisplayModule;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  displayedColumns: string[] = ['title', 'description', 'deadline', 'checklistDeadline', 'state', 'grade','solution', 'check', 'symbol'];
  isTeacher=false;
  constructor( private assignedTService: AssignedTaskService,private authService: AuthService,public listService:ListService,  private dialog: MatDialog) {
    this.isTeacher=this.authService.decodeJWToken("role")==="STUDENT"?false:true;
  }

  ngOnInit(): void {
  }


  onExpand() {
   
    this.panelOpenState = true
    if (this.dataSource == null || this.dataSource.length <= 0) {
      this.assignedTService.GetAssignedTasks(this.list.key).subscribe(cr => {
        this.dataSource =new MatTableDataSource<AssignedTaskModule>(cr as AssignedTaskModule[]);
        this.dataSource.paginator = this.paginator;
      });
    }
  }
  onCheckList(id:any,status:any,statusD:any){
    const diagRef = this.dialog.open(MainCheckDialogComponent, {  width: '60%',height:'80%',data: { data:id, status: status, statusDeadline:statusD} });
  }
  onCreateTask() {
    const diagRef = this.dialog.open(SelectAssignmentComponent, {  width: '60%',data: { data: this.list.key } });
    this.dataSource = null;
  }
  onUpdate(id:any){
    const diagRef = this.dialog.open(UpdateAssignedTaskComponent, { data: { data: id } });
    this.dataSource = null;
  }
  onGoTo(link:any){
    const diagRef = this.dialog.open(RedirectSolutionDialogComponent, {width: '40%', data: { data: link } });
  }
  onDeleteList(): void {
    if (confirm("Are you sure?")) {
      this.listService.DeleteList(this.list.key).subscribe(cr => {
        this.listService.alertService.showSucces('The list was removed!');
        window.location.reload();
      });
    }
  }
}
