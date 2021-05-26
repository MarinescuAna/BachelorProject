import { Component, Injector, Input, OnInit, ViewChild } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { MatDialog } from '@angular/material/dialog';
import { NavigationExtras, Router } from '@angular/router';
import { from } from 'rxjs';
import { ListDisplayModule } from 'src/app/modules/list-display.module';
import { ListService } from 'src/app/services/list.service';
import { SheetKeyComponent } from '../../group-section/my-groups/sheet-key/sheet-key.component';
import { CreateTaskComponent } from '../create-task/create-task.component';
import { AssignmentsListModule } from 'src/app/modules/assigments-list.module';
import { AssignmentService } from 'src/app/services/assignment.service';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  panelOpenState = false;
  dataSource: AssignmentsListModule[];
  @Input() list: ListDisplayModule;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  displayedColumns: string[] = ['title', 'description', 'groupsMax', 'groupsTake', 'deadline', 'checklistDeadline', 'createdDate', 'symbol'];
  displayTable = false;
  constructor(private _bottomSheet: MatBottomSheet, private service: ListService, private assignmentService: AssignmentService, private route: Router, private dialog: MatDialog) {
  }

  ngOnInit(): void {
  }

  openKeySheet(): void {
    this._bottomSheet.open(SheetKeyComponent, { data: { key: this.list.key } });
  }

  onExpand() {
    this.panelOpenState = true
    if (this.dataSource==null || this.dataSource.length <= 0) {
      this.assignmentService.GetAssignments(this.list.key).subscribe(cr => {
        this.dataSource = cr as AssignmentsListModule[];
      });
    }
    this.displayTable = true;
  }

  onCreateTask() {
    const diagRef = this.dialog.open(CreateTaskComponent, { data: { data: this.list } });
  }
  viewGroup(): void {
    let navigationExtras: NavigationExtras = {
      queryParams: this.list
    };
    this.route.navigate(['\list-details'], navigationExtras);
  }

  onLeaveGroup(): void {
    /*if (confirm("Are you sure?")) {
      this.service.LeaveGroup(this.group.uniqueKey).subscribe(cr => {
        this.alertService.showSucces('You left the group successfully!');
        window.location.reload();
      });
    }*/

  }
}
