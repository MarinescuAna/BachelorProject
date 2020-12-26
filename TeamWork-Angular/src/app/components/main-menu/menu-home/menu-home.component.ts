import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from 'src/app/shared/auth.service';

@Component({
  selector: 'app-menu-home',
  templateUrl: './menu-home.component.html',
  styleUrls: ['./menu-home.component.css']
})
export class MenuHomeComponent  {

  @ViewChild('clickMe') clickMe: any;

  email: string;

  clickOnHover(): void {
    this.clickMe._elementRef.nativeElement.click();
  }



}
