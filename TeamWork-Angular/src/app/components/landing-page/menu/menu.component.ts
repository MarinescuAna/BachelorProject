import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { NotificationService } from 'src/app/services/notification.service';
import { AuthService } from 'src/app/shared/auth.service';
import { MainNotificationDialogComponent } from '../landing-pages-components/notification/main-notification-dialog/main-notification-dialog.component';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {

  notify:number=0;
  email: string;
  isTeacher=false;

  constructor(public authService: AuthService, private route: Router, private dialog: MatDialog,private notifyService:NotificationService) {
    this.email = this.authService.decodeJWRefreshToken('unique_name');
    this.isTeacher=this.authService.decodeJWToken("role")==="STUDENT"?false:true;
    this.notifyService.GetNotificationsNumber().subscribe(cr =>{
      this.notify= cr as number;
    } );
  }

  onSubmit(): void {
    this.authService.doLogout();
  }

  redirectTo(url: string): void {
    this.route.navigateByUrl(url);
  }

  redirectHome() {
      this.route.navigateByUrl('landing-page')
  }

  onViewNotifications(){
    if(this.notify>0)
    {const diagRef = this.dialog.open(MainNotificationDialogComponent, { width: '60%' , height:'60%'});
  }
}
}