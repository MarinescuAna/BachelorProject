import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { CreateListModule } from 'src/app/modules/create-list.module';
import { ListService } from 'src/app/services/list.service';

@Component({
  selector: 'app-create-list-student',
  templateUrl: './create-list-student.component.html',
  styleUrls: ['./create-list-student.component.css']
})
export class CreateListStudentComponent implements OnInit {

  
  formlist = new FormGroup({
    title: new FormControl('',[Validators.required]),
    domain: new FormControl('',[Validators.required])
  });
  group:string;
  constructor(private listService: ListService, private route:Router,@Optional() @Inject(MAT_DIALOG_DATA) public data: any, ) {
    this.group=data.data as string;
   }

  ngOnInit(): void {
  }

  redirectTo(url: string): void {
    this.route.navigateByUrl(url);
  }
  
  onSubmit(): void{
    let list = new CreateListModule();
    list.domain=this.formlist.value.domain;
    list.title=this.formlist.value.title;
    list.groupId=this.group;
    this.listService.CreateList(list).subscribe(cr=>{
    this.listService.alertService.showSucces("The list was created!");
    window.location.reload();
  });
}
}
