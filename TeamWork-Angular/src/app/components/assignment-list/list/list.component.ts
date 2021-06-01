import { Component, Input, OnInit } from '@angular/core';
import { ListDisplayModule } from 'src/app/modules/list-display.module';


@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  @Input() list: ListDisplayModule;

   constructor() {
  }

  ngOnInit(): void {
  }


  
}
