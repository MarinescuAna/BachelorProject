import { Injectable, Injector } from '@angular/core';
import { DataService } from '../services/data.service';

@Injectable({
  providedIn: 'root'
})
export class ChecklistGradeService  extends DataService {
  constructor(injector: Injector) {
    super(injector, 'ChecklistGrade');
  }

  ReturnCheckListGrade(data: any): any {
    return super.post<any>('ReturnCheckListGrade?assignmentId='+data,'');
  }

  GetChecklistGrade(data: any): any {
    return super.getMany('GetChecklistGrade?takeGradeChecklist='+data);
  }

}
