import { Component, Input, OnInit } from '@angular/core';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {

  @Input() group: ViewGroupsModule;
  constructor() { }

  ngOnInit(): void {
  }

}
