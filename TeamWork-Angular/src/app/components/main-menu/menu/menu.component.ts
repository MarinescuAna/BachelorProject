import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/shared/auth.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {
  url="\src\assets\audio\music.mp3";
  isPlay=false;
  @ViewChild('clickMe') clickMe: any;
  email: string;
  audioList = [
    {
      url: "https://www.soundhelix.com/examples/mp3/SoundHelix-Song-1.mp3",
      title: "Smaple 1"
    },
    {
      url: "https://www.soundhelix.com/examples/mp3/SoundHelix-Song-10.mp3",
      title: "Sample 2",
    },
    {
      url: "https://www.soundhelix.com/examples/mp3/SoundHelix-Song-12.mp3",
      title: "Sample 3",
    }
  ];

  constructor(public authService: AuthService, private route: Router) {
    this.email = this.authService.decodeJWRefreshToken('unique_name');
  }

  onSubmit(): void {
    this.authService.doLogout();
  }

  clickOnHover() {
    this.clickMe._elementRef.nativeElement.click();
  }

  redirectTo(url: string): void {
    this.route.navigateByUrl(url);
  }

  redirectHome() {
    if (this.authService.isLogged()) {
      this.route.navigateByUrl('home-logged')
    }
    else {
      this.route.navigateByUrl('landing-page')
    }
  }
}