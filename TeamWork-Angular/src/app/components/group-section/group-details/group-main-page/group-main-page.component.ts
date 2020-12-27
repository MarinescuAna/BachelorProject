import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';

@Component({
  selector: 'app-group-main-page',
  templateUrl: './group-main-page.component.html',
  styleUrls: ['./group-main-page.component.css']
})
export class GroupMainPageComponent implements OnInit {

  public group:ViewGroupsModule;
  constructor(private route:ActivatedRoute) {
    debugger
    this.route.queryParams.subscribe(
      params=>{
        this.group=params as ViewGroupsModule;
      }
    );
   }

  ngOnInit(): void {
  }

}
