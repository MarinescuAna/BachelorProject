import { Component, Input, OnInit } from '@angular/core';
import { GroupSendModule } from 'src/app/modules/group-send.module';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {

  @Input() group: GroupSendModule;
  constructor() { }

  ngOnInit(): void {
  }

}
