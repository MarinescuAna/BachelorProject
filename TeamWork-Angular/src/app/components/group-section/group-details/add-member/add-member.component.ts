import { Component, Inject, Injector, OnInit, Optional } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AddMemberModule } from 'src/app/modules/add-member.module';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';
import { AlertService } from 'src/app/services/alert.service';
import { GroupService } from 'src/app/services/group-service';

@Component({
  selector: 'app-add-member',
  templateUrl: './add-member.component.html',
  styleUrls: ['./add-member.component.css']
})
export class AddMemberComponent implements OnInit {

  private pattern =/^([^@\s]+)@((?:[-a-z0-9]+\.)+[a-z]{2,})$/i;
  formGroup = new FormGroup({
    email: new FormControl('', [Validators.required,Validators.pattern(this.pattern)])
  });
  group:ViewGroupsModule;
  constructor(private injector: Injector,
    private groupService: GroupService,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any) {
    this.group=this.data; 
  }

  ngOnInit(): void {
  }

  onSubmit(): void {    
    debugger;
    const temp = new AddMemberModule();
    temp.attenderEmail = this.formGroup.value.email;
    temp.groupKey = this.group['data'].uniqueKey;

    this.groupService.AddMember(temp).subscribe(cr => {
      this.injector.get(AlertService).showSucces('The invitation has been sent!');
      window.location.reload();
    });;
  }
}
