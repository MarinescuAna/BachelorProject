import { Component, OnInit } from '@angular/core';
import{ViewGroupsModule} from 'src/app/modules/view-groups.module';
import { GroupService } from 'src/app/services/group-service';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  text='You haven\'t joined any groups so far. Create your own group or join an existing group by accessing the Manage_Groups menu.';
  length:any;
  myGroups: ViewGroupsModule[];
  constructor(private groupService: GroupService) {

   }

  ngOnInit(): void {
    debugger
    this.groupService.GetMyGroupsStudent(0).subscribe(cr =>{
      this.myGroups= cr as ViewGroupsModule[];
      this.length=this.myGroups.length;
    });
  }

}
