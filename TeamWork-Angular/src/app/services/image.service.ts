import { Injectable, Injector } from '@angular/core';
import { DataService } from '../services/data.service';

@Injectable({
  providedIn: 'root'
})
export class ImageService  extends DataService {
  constructor(injector: Injector) {
    super(injector, 'Image');
  }

  InsertUserImage(message: any): any {
    return super.post<any>('InsertUserImage', message);
  }

}
