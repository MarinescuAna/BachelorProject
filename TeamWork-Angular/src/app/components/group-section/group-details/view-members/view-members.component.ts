import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { DeleteMemberModule } from 'src/app/modules/delete-member.module';
import { GroupService } from 'src/app/services/group-service';
import { ViewMembersModule } from 'src/app/modules/view-members.module';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';
import { AuthService } from 'src/app/shared/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { ProfilePageDialogComponent } from '../profile-page-dialog/profile-page-dialog.component';

@Component({
  selector: 'app-view-members',
  templateUrl: './view-members.component.html',
  styleUrls: ['./view-members.component.css']
})
export class ViewMembersComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  displayedColumns: string[];
  displayedColumnsTeacher: string[] = ['name', 'email', 'institution', 'role','profile'];
  displayedColumnsStudent: string[] = ['name', 'email', 'institution', 'role', 'profile','symbol'];
  dataSource: any;
  isStudent = true;
  ngOnInit() {
    this.groupService.ViewMembers(this.group.uniqueKey).subscribe(cr => {
      this.dataSource = new MatTableDataSource<ViewMembersModule>(cr as ViewMembersModule[]);
      this.dataSource.paginator = this.paginator;
    }
    );
  }
  @Input() group: ViewGroupsModule;
  constructor(private groupService: GroupService, private authService: AuthService,private dialog: MatDialog) {
    this.isStudent = this.authService.decodeJWToken('role').toLowerCase() === 'student';
    if (this.isStudent === false) {
      this.displayedColumns = this.displayedColumnsTeacher;
    } else {
      this.displayedColumns = this.displayedColumnsStudent;
    }

  }
  onUser(id:any):void{
    const diagRef = this.dialog.open(ProfilePageDialogComponent, { width: 'auto', height:'auto', data: id});
  }

  deleteUser(id: any): void {
    if (confirm("Are you sure?")) {
      let user = new DeleteMemberModule();
      user.email = id;
      user.groupKey = this.group.uniqueKey;
      this.groupService.GetOutMember(user).subscribe(cr => {
        window.location.reload();
      })
    }
  }

}
