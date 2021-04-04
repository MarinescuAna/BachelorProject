import { Component, Injector, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { GroupCreateModule } from 'src/app/modules/group-create.module'; 
import { AuthService } from 'src/app/shared/auth.service';
import { GroupService } from 'src/app/services/group-service';
import { AlertService } from 'src/app/services/alert.service';
import { Router } from '@angular/router';

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
    description: new FormControl()
  });
  constructor(private authService: AuthService,private groupService: GroupService, private injector: Injector, private route: Router) { }

  ngOnInit(): void {
  }
  redirectTo(url: string): void {
    this.route.navigateByUrl(url);
  }
  onSubmit(): void{
    const temp=new GroupCreateModule();
    temp.description=this.formCreateGroup.value.description;
    temp.groupName=this.formCreateGroup.value.name;
    temp.studentCreatorEmail=this.authService.decodeJWRefreshToken('email');
    temp.teacherEmail=this.formCreateGroup.value.emailTeacher;
    this.groupService.CreateNewGroup(temp).subscribe(cr => {
      this.injector.get(AlertService).showSucces('The group was created!');
      this.route.navigateByUrl('/my-groups');
    });
  }
}
