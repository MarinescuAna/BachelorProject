import { Component, Inject, Injector, OnInit, Optional } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { GroupUpdateModule } from 'src/app/modules/group-update.module';
import { GroupService } from 'src/app/services/group-service';
import { AuthService } from 'src/app/shared/auth.service';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';
import { AlertService } from 'src/app/services/alert.service';

@Component({
  selector: 'app-edit-group',
  templateUrl: './edit-group.component.html',
  styleUrls: ['./edit-group.component.css']
})
export class EditGroupComponent implements OnInit {

  formCreateGroup: FormGroup;
  group:ViewGroupsModule;
  constructor(private injector: Injector,
    private groupService: GroupService,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any) {
    this.group=this.data.data;
    this.formCreateGroup = new FormGroup({
      name: new FormControl(this.group.groupName, [Validators.required]),
      emailTeacher: new FormControl(this.group.teacherEmail),
      description: new FormControl(this.group.groupDetails)
    });
  }

  ngOnInit(): void {
  }

  onSubmit(): void {    
    if(this.group.groupName == this.formCreateGroup.value.name &&
    this.group.groupDetails == this.formCreateGroup.value.description){
      return;
    }

    const temp = new GroupUpdateModule();
    temp.description = this.formCreateGroup.value.description;
    temp.groupName = this.formCreateGroup.value.name;
    temp.id = this.group.uniqueKey;
    temp.oldGroupName = this.group.groupName;

    this.groupService.UpdateGroup(temp).subscribe(cr => {
      this.injector.get(AlertService).showSucces('The group was change!');
      window.location.reload();
    });;
  }
}
