import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { AssignedTasksPerGroupModule } from 'src/app/modules/assigned-tasks-per-group.module';
import { ListDisplayModule } from 'src/app/modules/list-display.module';
import { AssignedTaskService } from 'src/app/services/assigned-task.service';
import { MainCheckDialogComponent } from '../../checklist-section/main-check-dialog/main-check-dialog.component';
import { RedirectSolutionDialogComponent } from '../../group-section/redirect-solution-dialog/redirect-solution-dialog.component';
import { UpdateAssignedTaskComponent } from '../../group-section/update-assigned-task/update-assigned-task.component';

@Component({
  selector: 'app-groups-view-extention',
  templateUrl: './groups-view-extention.component.html',
  styleUrls: ['./groups-view-extention.component.css']
})
export class GroupsViewExtentionComponent implements OnInit {
  
  panelOpenStateGroup = false;
  dataSource: any;
  displayedColumns: string[] = ['name', 'title', 'status', 'deadline', 'checklistDeadline', 'solution', 'grade', 'addgrade', 'symbol'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @Input() list: ListDisplayModule;

  constructor(
    private assignTaskService: AssignedTaskService,
    private dialog: MatDialog,) { }

  ngOnInit(): void {
  }

  onExpandGroupSection() {
    this.panelOpenStateGroup = true;
    if (this.dataSource == null || this.dataSource.length <= 0) {
      this.assignTaskService.GetTasksPerGroup(this.list.key).subscribe(cr => {
        this.dataSource = new MatTableDataSource<AssignedTasksPerGroupModule>(cr as AssignedTasksPerGroupModule[]);
        this.dataSource.paginator = this.paginator;
      });
    }
  }
  onUpdate(id: any) {
    const diagRef = this.dialog.open(UpdateAssignedTaskComponent, { data: { data: id } });
    this.dataSource = null;
  }

  onCheckList(id: any,status:any,statusD:any){
    const diagRef = this.dialog.open(MainCheckDialogComponent, {  width: '60%',height:'100%',data: { data:id, status: status, statusDeadline:statusD} });

  }
  
  onGoTo(link: any) {
    const diagRef = this.dialog.open(RedirectSolutionDialogComponent, { width: '40%', data: { data: link } });
  }
}
