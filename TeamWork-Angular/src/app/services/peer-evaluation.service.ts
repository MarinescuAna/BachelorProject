import { Injectable, Injector } from '@angular/core';
import { DataService } from '../services/data.service';

@Injectable({
  providedIn: 'root'
})
export class PeerEvaluationService  extends DataService {
  constructor(injector: Injector) {
    super(injector, 'PeerEvaluation');
  }

  GetMemberForEvaluation(data: any): any {
    return super.getMany<any>('GetMemberForEvaluation?text='+data);
  }

  AssignPeerEvaluation(data:any):any{
    return super.update('AssignPeerEvaluation',data);
  }

  GetGrade(data:any):any{
    return super.getOne<any>('GetGrade?assignedTaskId='+data);
  }
}
