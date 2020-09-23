import { Component, ViewChild } from '@angular/core';

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

}
