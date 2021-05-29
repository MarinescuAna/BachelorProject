import { Component, Input, OnInit } from '@angular/core';
import { ListDisplayModule } from 'src/app/modules/list-display.module';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';
import { ListService } from 'src/app/services/list.service';

@Component({
  selector: 'app-assignment-list',
  templateUrl: './assignment-list.component.html',
  styleUrls: ['./assignment-list.component.css']
})
export class AssignmentListComponent implements OnInit {

  @Input() group: ViewGroupsModule;
  myLists: ListDisplayModule[];
  constructor(private listService: ListService) {

   }

  ngOnInit(): void {
    this.listService.GetLists(this.group.uniqueKey).subscribe(cr =>{
      this.myLists= cr as ListDisplayModule[];
    });
  }
}
