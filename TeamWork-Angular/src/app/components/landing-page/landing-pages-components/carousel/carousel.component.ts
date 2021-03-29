import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css']
})
export class CarouselComponent implements OnInit {

  @Input() slides: any;
  constructor() { }
  ngOnInit(): void {
  }
  
  slideConfig = { "slidesToShow": 1, autoplay: true, speed: 350, "slidesToScroll": 1 };


}
