import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { ImageModule } from 'src/app/modules/image.module';
import { AlertService } from 'src/app/services/alert.service';
import { ImageService } from 'src/app/services/image.service';
import { AuthService } from 'src/app/shared/auth.service';
import { UserInfoModule } from 'src/app/modules/user-info.module';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ChangePasswordModule } from 'src/app/modules/change-password.module';

declare const L: any;

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css']
})
export class ProfilePageComponent implements OnInit {
  wasChange = true;
  src: string;
  userInfo = new UserInfoModule();
  fileName: string;
  newRecord = new ImageModule();
  userInfoForm: FormGroup;
  myThumbnail: string;
  myFullresImage: string;
  passwordForm = new FormGroup({
    old: new FormControl('', [Validators.required, Validators.minLength(6)]),
    new: new FormControl('', [Validators.required, Validators.minLength(6)]),
    confirm: new FormControl('', [Validators.required, Validators.minLength(6)]),
  });
  constructor(private injector: Injector, private imageService: ImageService, private userService: AuthService) {

    this.userService.GetUserInfo('').subscribe(cr => {
      this.userInfo = cr as UserInfoModule;
      if (this.userInfo == null || this.userInfo.imageContent == null) {
        this.src = 'assets/Images/profile.jpg';
      } else {
        this.src = 'data:image/' + this.userInfo.imageExtention + ';base64,' + this.userInfo.imageContent;
      }
      this.myFullresImage = this.myThumbnail = this.src;

      this.userInfoForm = new FormGroup({
        email: new FormControl(this.userInfo.email),
        firstName: new FormControl(this.userInfo.firstName, [Validators.required]),
        lastName: new FormControl(this.userInfo.lastName, [Validators.required]),
        role: new FormControl(this.userInfo.role),
        institution: new FormControl(this.userInfo.institution, [Validators.required]),
      });
    });
  }

  ngOnInit(): void {


  }

  onSubmit() {
    let newUser = new UserInfoModule();
    newUser.email=this.userInfoForm.value.email;
    let count = 0;

    if (this.userInfoForm.value.lastName != this.userInfo.lastName) {
      count++;
      newUser.lastName = this.userInfoForm.value.lastName;
    }
    if (this.userInfoForm.value.firstName != this.userInfo.firstName) {
      count++;
      newUser.firstName = this.userInfoForm.value.firstName;
    }
    if (this.userInfoForm.value.institution != this.userInfo.institution) {
      count++;
      newUser.institution = this.userInfoForm.value.institution;
    }

    if (count != 0) {
      this.userService.UpdateUserInfo(newUser).subscribe(cr => {
        this.userService.alertService.showSucces("Sucess!");
      });
    }else{
      this.userService.alertService.showWarning("No changes!");
    }
  }

  onChangePassword() {
    let change = new ChangePasswordModule();
    if (this.passwordForm.value.old == this.passwordForm.value.new) {
      this.userService.alertService.showWarning("You must enter a new password!");
      return;
    }

    if (this.passwordForm.value.confirm != this.passwordForm.value.new) {
      this.userService.alertService.showWarning("New passwords do not match!");
      return;
    }

    change.newPassword = this.passwordForm.value.new;
    change.oldPassword = this.passwordForm.value.old;
    this.userService.ChangePassword(change).subscribe(cr => {
      this.userService.alertService.showSucces("Sucess!");
      window.location.reload();
    });
  }

  onSave() {
    this.imageService.InsertUserImage(this.newRecord).subscribe(cr => {
      window.location.reload();
    });
  }
  onFileChange(event): void {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      this.fileName = event.target.files[0].name;
      if (this.fileName.split('.')[1] === 'jpg' || this.fileName.split('.')[1] === 'png' || this.fileName.split('.')[1] === 'jpeg') {
        this.wasChange = false;
        const [file] = event.target.files;
        reader.readAsDataURL(file);
        reader.onload = () => {
          this.newRecord.imageExtention = this.fileName.split('.')[1];
          this.newRecord.imageContent = reader.result.toString().split(',')[1];
        };
      } else {
        this.injector.get(AlertService).showError('Invalid file! You can only send jpg, png and jpeg files!');
      }
    }
  }
}
