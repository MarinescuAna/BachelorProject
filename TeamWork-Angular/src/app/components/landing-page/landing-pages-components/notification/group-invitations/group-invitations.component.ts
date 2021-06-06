import { Component, OnInit } from '@angular/core';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';
import { GroupService } from 'src/app/services/group-service';
import{NotificationService} from 'src/app/services/notification.service';
import {NotificationModule} from 'src/app/modules/notification.module';

@Component({
  selector: 'app-group-invitations',
  templateUrl: './group-invitations.component.html',
  styleUrls: ['./group-invitations.component.css']
})
export class GroupInvitationsComponent implements OnInit {

  myGroups: ViewGroupsModule[];
  notifications: NotificationModule[];
  constructor(private groupService: GroupService, private notificationService:NotificationService) {

   }

  ngOnInit(): void {
    this.groupService.GetMyGroupsStudent(1).subscribe(cr =>{
      this.myGroups= cr as ViewGroupsModule[];
    });
    this.notificationService.GetNotifications().subscribe(cr=>{
      this.notifications=cr as NotificationModule[];
    });
  }
}
