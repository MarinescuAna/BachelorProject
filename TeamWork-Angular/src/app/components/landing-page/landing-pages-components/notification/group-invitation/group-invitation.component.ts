import { Component, Injector, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NotificationModule } from 'src/app/modules/notification.module';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';
import { AlertService } from 'src/app/services/alert.service';
import { GroupService } from 'src/app/services/group-service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-group-invitation',
  templateUrl: './group-invitation.component.html',
  styleUrls: ['./group-invitation.component.css']
})
export class GroupInvitationComponent implements OnInit {


  display = true;
  @Input() group: ViewGroupsModule;
  @Input() state: any;
  @Input() notification: NotificationModule;

  constructor(private groupService: GroupService, private notificationService: NotificationService) {
  }

  ngOnInit(): void {
  }

  onMarkAsSeen() {
    this.notificationService.MarkAsSeen(this.notification.id).subscribe(cr => {
      this.display = false;

    });
  }
  onAccept(): void {
    this.groupService.Accept(this.group.uniqueKey).subscribe(cr => {
      this.groupService.alertService.showSucces('Success. Welcome to the group!');
      this.display = false;
    });
  }

  onDecline(): void {
    if (confirm("Are you sure?")) {
      this.groupService.LeaveGroup(this.group.uniqueKey).subscribe(cr => {
        this.display = false;
      });
    }
  }
}
