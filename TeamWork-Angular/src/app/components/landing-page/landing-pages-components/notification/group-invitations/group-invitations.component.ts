import { Component, OnInit } from '@angular/core';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';
import { GroupService } from 'src/app/services/group-service';

@Component({
  selector: 'app-group-invitations',
  templateUrl: './group-invitations.component.html',
  styleUrls: ['./group-invitations.component.css']
})
export class GroupInvitationsComponent implements OnInit {
  length:any;
  myGroups: ViewGroupsModule[];
  constructor(private groupService: GroupService) {

   }

  ngOnInit(): void {
    this.groupService.GetMyGroupsStudent(1).subscribe(cr =>{
      this.myGroups= cr as ViewGroupsModule[];
      this.length=this.myGroups.length;
    });
  }
}
