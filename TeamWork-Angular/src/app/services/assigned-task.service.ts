import { Injectable, Injector } from '@angular/core';
import { DataService } from '../services/data.service';

@Injectable({
  providedIn: 'root'
})
export class AssignedTaskService  extends DataService {
  constructor(injector: Injector) {
    super(injector, 'AssignedTask');
  }

  public GetAssignedTasks(list:string):any{ 
    return super.getMany<any>('GetTasksGroup?listId='+list);
  }

  public GetTasksPerGroup(list:string):any{ 
    return super.getMany<any>('GetTasksPerGroup?listId='+list);
  }

  public AssignTask(task:any):any{
    return super.post<any>('AssignTask', task);
  }

  public UpdateAssignedTask(data: any): any {
    return super.update('UpdateAssignedTask', data);
  }
  DeleteTask(id: any): any {
    return super.delete("id=" + id, 'DeleteTask');
  }
}