import { Component, Injector, Input, OnInit } from '@angular/core';
import {  Router } from '@angular/router';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';
import { AlertService } from 'src/app/services/alert.service';
import { GroupService } from 'src/app/services/group-service';

@Component({
  selector: 'app-group-invitation',
  templateUrl: './group-invitation.component.html',
  styleUrls: ['./group-invitation.component.css']
})
export class GroupInvitationComponent implements OnInit {


  @Input() group: ViewGroupsModule;
  private alertService: any;
  constructor(private service: GroupService, private injector: Injector, private route: Router) {
    this.alertService = this.injector.get(AlertService);
  }

  ngOnInit(): void {
  }

  onAccept(): void {
    this.service.Accept(this.group.uniqueKey).subscribe(cr => {
      this.alertService.showSucces('Success. Welcome to the group!');
      window.location.reload();
    });
  }

  onDecline(): void {
    if (confirm("Are you sure?")) {
      this.service.LeaveGroup(this.group.uniqueKey).subscribe(cr => {
        window.location.reload();
      });
    }
  }
}
