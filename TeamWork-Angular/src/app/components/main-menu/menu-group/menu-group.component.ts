import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu-group',
  templateUrl: './menu-group.component.html',
  styleUrls: ['./menu-group.component.css']
})
export class MenuGroupComponent  {
  @ViewChild('clickMe') clickMe: any;

  clickOnHover() {
    this.clickMe._elementRef.nativeElement.click();
  }

  constructor(private route: Router){

  }

  redirectTo(url:string):void{
    this.route.navigateByUrl(url);
  }
}
