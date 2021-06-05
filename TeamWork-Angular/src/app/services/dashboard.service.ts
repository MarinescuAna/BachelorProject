import { Injectable, Injector } from '@angular/core';
import { DataService } from '../services/data.service';

@Injectable({
  providedIn: 'root'
})
export class DashboardService  extends DataService {
  constructor(injector: Injector) {
    super(injector, 'Dashboard');
  }

  GetReport(data: any): any {
    return super.getMany('GetReport?text='+data);
  }
  

}
