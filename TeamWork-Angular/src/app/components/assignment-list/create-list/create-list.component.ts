import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {ListService} from 'src/app/services/list.service';
import {CreateListModule} from 'src/app/modules/create-list.module';

@Component({
  selector: 'app-create-list',
  templateUrl: './create-list.component.html',
  styleUrls: ['./create-list.component.css']
})
export class CreateListComponent implements OnInit {

  formlist = new FormGroup({
    title: new FormControl('',[Validators.required]),
    domain: new FormControl('',[Validators.required]),
    deadline: new FormControl('',[])
  });
  constructor(private listService: ListService, private route:Router) { }

  ngOnInit(): void {
  }

  redirectTo(url: string): void {
    this.route.navigateByUrl(url);
  }
  
  onSubmit(): void{
    let list = new CreateListModule();
    list.domain=this.formlist.value.domain;
    list.title=this.formlist.value.title;
    list.listDeadline=this.formlist.value.deadline;
    list.groupId="";
    this.listService.CreateList(list).subscribe(cr=>{
    this.route.navigateByUrl('/list');
    this.listService.alertService.showSucces("The list was created!");
  });
}

}
