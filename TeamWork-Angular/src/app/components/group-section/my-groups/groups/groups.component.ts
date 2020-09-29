import { Component, OnInit } from '@angular/core';
import{GroupSendModule} from 'src/app/modules/group-send.module';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  myGroups: GroupSendModule[];
  constructor() { }

  ngOnInit(): void {
  }

}
