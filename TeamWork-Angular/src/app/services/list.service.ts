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

  public GetLists():any{
    return super.getMany<any>('GetLists');
  }

}