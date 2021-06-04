import { Component, Input, OnInit } from '@angular/core';
import { ListDisplayModule } from 'src/app/modules/list-display.module';
import { AuthService } from 'src/app/shared/auth.service';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';

@Component({
  selector: 'app-assignment',
  templateUrl: './assignment.component.html',
  styleUrls: ['./assignment.component.css']
})

export class AssignmentComponent implements OnInit {

  @Input() list: ListDisplayModule;
  @Input() group: ViewGroupsModule;
  isTeacher=false;
  constructor(     private authService: AuthService) {
    this.isTeacher = this.authService.decodeJWToken("role") === "STUDENT" ? false : true;
  }

  ngOnInit(): void {
  }




}
