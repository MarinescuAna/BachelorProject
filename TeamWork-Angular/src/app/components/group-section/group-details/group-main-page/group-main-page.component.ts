import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';
import { AuthService } from 'src/app/shared/auth.service';
import { AddMemberComponent } from '../add-member/add-member.component';
import { EditGroupComponent } from '../edit-group/edit-group.component';

@Component({
  selector: 'app-group-main-page',
  templateUrl: './group-main-page.component.html',
  styleUrls: ['./group-main-page.component.css']
})
export class GroupMainPageComponent implements OnInit {

  isStudent=true;
  public collapseMembers = true;
  public group: ViewGroupsModule;
  constructor(private dialog: MatDialog, private route: ActivatedRoute, private router: Router, private authService: AuthService) {
    this.isStudent = this.authService.decodeJWToken('role').toLowerCase() === 'student';
    this.route.queryParams.subscribe(
      params => {
        this.group = params as ViewGroupsModule;
      }
    );
  }

  ngOnInit(): void {
  }
  redirectTo(url: string): void {
    this.router.navigateByUrl(url);
  }
  openDialog(): void {
    const diagRef = this.dialog.open(EditGroupComponent, { data: { data: this.group } });
  }

  openDialogAddMember(): void {
    const diagRef = this.dialog.open(AddMemberComponent, { data: { data: this.group } });
  }


  redirectToMembers(): void {
    this.collapseMembers = !this.collapseMembers;
  }
}
