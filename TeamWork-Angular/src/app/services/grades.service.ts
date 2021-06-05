import { Injectable, Injector } from '@angular/core';
import { DataService } from '../services/data.service';

@Injectable({
  providedIn: 'root'
})
export class GradeService  extends DataService {
  constructor(injector: Injector) {
    super(injector, 'Grade');
  }

  GetGrade(data: any): any {
    return super.getMany('GetGrades?listId='+data);
  }
  GetCurrentUserGrades(data: any): any {
    return super.getMany('GetCurrentUserGrades?listId='+data);
  }
  

}
