import { Component, Input, OnInit } from '@angular/core';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';

@Component({
  selector: 'app-group-details-section',
  templateUrl: './group-details-section.component.html',
  styleUrls: ['./group-details-section.component.css']
})
export class GroupDetailsSectionComponent implements OnInit {
  @Input() group: ViewGroupsModule;
  constructor() { }

  ngOnInit(): void {
  }

}
