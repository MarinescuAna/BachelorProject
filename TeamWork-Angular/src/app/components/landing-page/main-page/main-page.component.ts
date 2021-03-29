import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {
  slidesAllFunctions = [
    { img: "/assets/Carousel/imag1.jpg" },
    { img: "/assets/Carousel/images2.jpg" },
    { img: "/assets/Carousel/images3.jpg" },
    { img: "/assets/Carousel/images4.jpg" },
    { img: "/assets/Carousel/images5.jpg" },
    { img: "/assets/Carousel/images1.jpg" },
  ];
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

  ngOnInit(): void {
  }
}
