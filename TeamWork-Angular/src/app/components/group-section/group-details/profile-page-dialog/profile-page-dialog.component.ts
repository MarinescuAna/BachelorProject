import { Component, Inject, Injector, OnInit, Optional } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ImageModule } from 'src/app/modules/image.module';
import { UserInfoModule } from 'src/app/modules/user-info.module';
import { ImageService } from 'src/app/services/image.service';
import { AuthService } from 'src/app/shared/auth.service';

@Component({
  selector: 'app-profile-page-dialog',
  templateUrl: './profile-page-dialog.component.html',
  styleUrls: ['./profile-page-dialog.component.css']
})
export class ProfilePageDialogComponent implements OnInit {
  src: string;
  userInfo = new UserInfoModule();
  fileName: string;
  email: string;
  constructor(@Optional() @Inject(MAT_DIALOG_DATA) public data: any,
    private userService: AuthService) {
    this.email = data as string;   
    
  }

  ngOnInit(): void {
    this.userService.GetUserInfo(this.email).subscribe(cr => {
      this.userInfo = cr as UserInfoModule;
      if (this.userInfo == null || this.userInfo.imageContent == null) {
        this.src = 'assets/Images/profile.jpg';
      } else {
        this.src = 'data:image/' + this.userInfo.imageExtention + ';base64,' + this.userInfo.imageContent;
      }
    });

  }
}
