import { Component, Inject, OnInit, Optional } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import{ViewMembersModule} from 'src/app/modules/view-members.module';
import { GroupService } from 'src/app/services/group-service';

@Component({
  selector: 'app-main-check-dialog',
  templateUrl: './main-check-dialog.component.html',
  styleUrls: ['./main-check-dialog.component.css']
})
export class MainCheckDialogComponent implements OnInit {

  assignedTaskId:string;
  membersList: ViewMembersModule[];
  statusChk:string;
  statusD:string;
  constructor(@Optional() @Inject(MAT_DIALOG_DATA) public data: any,
    @Optional() @Inject(MAT_DIALOG_DATA) public status: any,
    @Optional() @Inject(MAT_DIALOG_DATA) public statusDeadline: any,
    public groupService:GroupService) {
    this.assignedTaskId=data.data as string;
    this.groupService.GetMembersByAssignedTaskIdKey(this.assignedTaskId).subscribe(cr=>{
      this.membersList=cr as ViewMembersModule[];
    });
    this.statusChk=status.status;
    this.statusD=statusDeadline.statusDeadline;
   }

  ngOnInit(): void {
  }

}
