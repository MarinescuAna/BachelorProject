import { Component, OnInit } from '@angular/core';
import{ViewGroupsModule} from 'src/app/modules/view-groups.module';
import { GroupService } from 'src/app/services/group-service';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  myGroups: ViewGroupsModule[];
  constructor(private groupService: GroupService) {

   }

  ngOnInit(): void {
    this.groupService.GetMyGroupsStudent(0).subscribe(cr =>{
      this.myGroups= cr as ViewGroupsModule[];
    });
  }

}
