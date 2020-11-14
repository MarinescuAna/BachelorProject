import { Component, Inject, Injector, Input, OnInit } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { Router } from '@angular/router';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';
import { AlertService } from 'src/app/services/alert.service';
import { GroupService } from 'src/app/services/group-service';
import { SheetKeyComponent } from '../sheet-key/sheet-key.component';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {

  @Input() group: ViewGroupsModule;
  private alertService:any;
  constructor(private _bottomSheet: MatBottomSheet, private service: GroupService,private injector: Injector, private route: Router) 
  { 
    this.alertService=this.injector.get(AlertService);
  }

  ngOnInit(): void {
  }

  openKeySheet():void{
    this._bottomSheet.open(SheetKeyComponent, {data:{key:this.group.uniqueKey}});
  }

  onLeaveGroup(){
    debugger
    this.service.LeaveGroup(this.group.uniqueKey).subscribe(cr => {
      this.alertService.showSucces('You left the group successfully!');
      this.route.navigateByUrl['\my-groups'];
    });;
  }
}
