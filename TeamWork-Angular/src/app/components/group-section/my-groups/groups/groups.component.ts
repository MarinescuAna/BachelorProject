import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import{ViewGroupsModule} from 'src/app/modules/view-groups.module';
import { GroupService } from 'src/app/services/group-service';
import { AuthService } from 'src/app/shared/auth.service';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  text='You haven\'t joined any groups so far. Create your own group or join an existing one.';
  length:any;
  myGroups: ViewGroupsModule[];
   isTeacher=false;
   constructor( private router: Router,
    private groupService: GroupService,
     private authService: AuthService ) {
       this.isTeacher = this.authService.decodeJWToken("role") === "STUDENT" ? false : true;
   }
 
   redirectTo(url: string): void {
     this.router.navigateByUrl(url);
   }

  ngOnInit(): void {
    this.groupService.GetMyGroupsStudent(0).subscribe(cr =>{
      this.myGroups= cr as ViewGroupsModule[];
      this.length=this.myGroups.length;
    });
  }

}
