import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import{ImageModule} from 'src/app/modules/image.module';
import { AlertService } from 'src/app/services/alert.service';
import { ImageService } from 'src/app/services/image.service';
import { AuthService } from 'src/app/shared/auth.service';
import { UserInfoModule } from 'src/app/modules/user-info.module';

declare const L: any;

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css']
})
export class ProfilePageComponent implements OnInit {
  wasChange=true;
  src:string;
  userInfo=new UserInfoModule();
  fileName:string;
  newRecord = new ImageModule();
  
  myThumbnail:string;
  myFullresImage:string;
  constructor(private injector: Injector, private imageService: ImageService, private userService:AuthService) {
  }

  ngOnInit(): void {
    this.userService.GetUserInfo().subscribe(cr =>{
      this.userInfo=cr as UserInfoModule;
      if(this.userInfo==null || this.userInfo.imageContent==null ){
        this.src='assets/Images/profile.jpg';
      }else{
        this.src='data:image/' + this.userInfo.imageExtention + ';base64,' + this.userInfo.imageContent;
      }
      this.myFullresImage=this.myThumbnail=this.src;
    });

    if (!navigator.geolocation) {
      console.log('location is not supported');
    }
    navigator.geolocation.getCurrentPosition((position) => {
      const coords = position.coords;
      const latLong = [coords.latitude, coords.longitude];
      console.log(
        `lat: ${position.coords.latitude}, lon: ${position.coords.longitude}`
      );
      let mymap = L.map('map').setView(latLong, 13);

      L.tileLayer(
        'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1Ijoic3VicmF0MDA3IiwiYSI6ImNrYjNyMjJxYjBibnIyem55d2NhcTdzM2IifQ.-NnMzrAAlykYciP4RP9zYQ',
        {
          attribution:
            'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
          maxZoom: 18,
          id: 'mapbox/streets-v11',
          tileSize: 512,
          zoomOffset: -1,
          accessToken: 'your.mapbox.access.token',
        }
      ).addTo(mymap);

      let marker = L.marker(latLong).addTo(mymap);

      marker.bindPopup('<b>Hi</b>').openPopup();

      let popup = L.popup()
        .setLatLng(latLong)
        .setContent('I am Subrat')
        .openOn(mymap);
    });
    this.watchPosition();
  }

  watchPosition() {
    let desLat = 0;
    let desLon = 0;
    let id = navigator.geolocation.watchPosition(
      (position) => {
        console.log(
          `lat: ${position.coords.latitude}, lon: ${position.coords.longitude}`
        );
        if (position.coords.latitude === desLat) {
          navigator.geolocation.clearWatch(id);
        }
      },
      (err) => {
        console.log(err);
      },
      {
        enableHighAccuracy: true,
        timeout: 5000,
        maximumAge: 0,
      }
    );
  }
  onSave(){
    this.imageService.InsertUserImage(this.newRecord).subscribe(cr =>{
      window.location.reload();
    });
  }
  onFileChange(event): void {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      this.fileName = event.target.files[0].name;
      if (this.fileName.split('.')[1] === 'jpg' || this.fileName.split('.')[1] === 'png' || this.fileName.split('.')[1] === 'jpeg') {
        this.wasChange=false;
        const [file] = event.target.files;
          reader.readAsDataURL(file);
          reader.onload = () => {
                                   this.newRecord.imageExtention = this.fileName.split('.')[1];
                                   this.newRecord.imageContent = reader.result.toString().split(',')[1];
          };
      }else{
        this.injector.get(AlertService).showError('Invalid file! You can only send jpg, png and jpeg files!');
      }
    }
  }
}
