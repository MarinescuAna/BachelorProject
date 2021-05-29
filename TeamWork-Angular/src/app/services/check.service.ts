import { Injectable, Injector } from '@angular/core';
import { DataService } from '../services/data.service';

@Injectable({
  providedIn: 'root'
})
export class CheckService  extends DataService {
  constructor(injector: Injector) {
    super(injector, 'Check');
  }

  InsertCheck(data: any): any {
    return super.post<any>('CreateCheck', data);
  }

  GetChecks(data: any): any {
    return super.getMany('GetChecks?text='+data);
  }

  DeleteCheck(id: any): any {
    return super.delete("checkId=" + id, 'DeleteCheck');
  }

  UpdateCheck(data: any): any {
    return super.update('UpdateCheck', data);
  }
  
  Check(data: any): any {
    return super.update('Check?checkId='+data,"");
  }
}
