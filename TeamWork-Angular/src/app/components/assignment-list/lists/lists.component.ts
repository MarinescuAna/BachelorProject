import { Component, OnInit } from '@angular/core';
import { ListDisplayModule } from 'src/app/modules/list-display.module';
import { ListService } from 'src/app/services/list.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {


  text='You haven\'t any lists. Create your own list.';
  length:any;
  myLists: ListDisplayModule[];
  constructor(private listService: ListService) {

   }

  ngOnInit(): void {
    this.listService.GetLists().subscribe(cr =>{
      this.myLists= cr as ListDisplayModule[];
      this.length=this.myLists.length;
    });
  }
}
