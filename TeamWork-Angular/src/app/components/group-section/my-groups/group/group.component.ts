import { Component, Input, OnInit } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';
import { SheetKeyComponent } from '../sheet-key/sheet-key.component';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {

  @Input() group: ViewGroupsModule;
  constructor(private _bottomSheet: MatBottomSheet) { }

  ngOnInit(): void {
  }

  openKeySheet():void{
    this._bottomSheet.open(SheetKeyComponent, {data:{key:this.group.uniqueKey}});
  }
}
