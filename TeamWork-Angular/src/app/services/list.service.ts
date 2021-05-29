import { Injectable, Injector } from '@angular/core';
import { DataService } from '../services/data.service';

@Injectable({
  providedIn: 'root'
})
export class ListService  extends DataService {
  constructor(injector: Injector) {
    super(injector, 'List');
  }

  public CreateList(message: any): any {
    return super.post<any>('CreateList', message);
  }
  public DeleteForeverList(id:any):any{
    return super.update('DeleteForeverList?listId='+id, '');
  }
  public DeleteList(id:any):any{
    return super.update('DeleteList?listId='+id, '');
  }
  public GetLists(groupId:string):any{
    return super.getMany<any>('GetLists?groupId='+groupId);
  }

}