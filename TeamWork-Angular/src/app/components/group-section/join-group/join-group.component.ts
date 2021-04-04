import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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
  constructor(private authService: AuthService,private groupService: GroupService, private route:Router) { }

  ngOnInit(): void {
  }
  redirectTo(url: string): void {
    this.route.navigateByUrl(url);
  }
  onSubmit(): void{
    this.groupService.JoinToGroup(this.formJoinGroup.value.key);
    window.location.reload();
  }
}
