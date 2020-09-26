import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { GroupCreateModule } from 'src/app/modules/group-create.module'; 
import { AuthService } from 'src/app/shared/auth.service';
import { GroupService } from 'src/app/services/group-service';

@Component({
  selector: 'app-create-group',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.css']
})
export class CreateGroupComponent implements OnInit {

  private pattern =/^([^@\s]+)@((?:[-a-z0-9]+\.)+[a-z]{2,})$/i;
  formCreateGroup = new FormGroup({
    name: new FormControl('',[Validators.required]),
    emailTeacher: new FormControl('',[Validators.required,Validators.pattern(this.pattern)]),
    description: new FormControl('')
  });
  constructor(private authService: AuthService,private groupService: GroupService) { }

  ngOnInit(): void {
  }

  onSubmit(): void{
    const temp=new GroupCreateModule();
    temp.description=this.formCreateGroup.value.description;
    temp.groupName=this.formCreateGroup.value.name;
    temp.studentCreatorEmail=this.authService.getUserEmail();
    temp.teacherEmail=this.formCreateGroup.value.emailTeacher;
    debugger
    this.groupService.CreateNewGroup(temp);
  }
}
