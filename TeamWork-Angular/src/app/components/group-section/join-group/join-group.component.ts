import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { GroupService } from 'src/app/services/group-service';
import { AuthService } from 'src/app/shared/auth.service';

@Component({
  selector: 'app-join-group',
  templateUrl: './join-group.component.html',
  styleUrls: ['./join-group.component.css']
})
export class JoinGroupComponent implements OnInit {
  formJoinGroup = new FormGroup({
    key: new FormControl('',[Validators.required])
  });
  constructor(private authService: AuthService,private groupService: GroupService) { }

  ngOnInit(): void {
  }

  onSubmit(): void{
    this.groupService.JoinToGroup(this.formJoinGroup.value.key);
    window.location.reload();
  }
}
