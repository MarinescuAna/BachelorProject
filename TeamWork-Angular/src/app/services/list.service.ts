import { Injectable, Injector } from '@angular/core';
import { DataService } from '../services/data.service';

@Injectable({
  providedIn: 'root'
})
export class ListService  extends DataService {
  constructor(injector: Injector) {
    super(injector, 'List');
  }

  CreateList(message: any): any {
    return super.post<any>('CreateList', message);
  }

}