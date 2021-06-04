import { Injectable, Injector } from '@angular/core';
import { DataService } from '../services/data.service';

@Injectable({
  providedIn: 'root'
})
export class AverageService  extends DataService {
  constructor(injector: Injector) {
    super(injector, 'Average');
  }

  public ComputeAverage(message: any): any {
    return super.post<any>('ComputeAverage', message);
  }

  public GetAssignments(list:string):any{ 
    return super.getMany<any>('GetTasks?listId='+list);
  }

  DeleteAssignment(id: any): any {
    return super.delete("assignmentId=" + id, 'DeleteAssignment');
  }

  MarkAsReturnChecklistGrades(data: any): any {
    return super.update('MarkAsReturnChecklistGrades?assignmentId='+data,'');
  }
}