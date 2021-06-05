import { Injectable, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { GroupCreateModule } from '../modules/group-create.module';
import { GroupCreateResponseModule } from '../modules/group-create-response.module';
import { DataService } from '../services/data.service';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class GroupService extends DataService {
  constructor(injector: Injector, private route: Router) {
    super(injector, 'Group');
  }

  CreateNewGroup(module: GroupCreateModule):any {
    return super.post<GroupCreateResponseModule>('CreateGroupByUser', module)
  }
  CreateGroupsRandom(module: any):any {
    return super.post<any>('CreateGroupsRandom', module)
  }
  SentInvitationsRandom(module: any):any {
    return super.post<any>('SentInvitationsRandom', module)
  }
  JoinToGroup(module: any) {
    super.post<any>('JoinToGroup?key='+module, {}).subscribe(cr => {
      this.alertService.showSucces('Success. Welcome to the group!');
    });
  }
  GetListGroups(id:any): Observable<any> {
    return super.getMany<any>('GetListGroups?listId='+id);
  }

  GetMyGroupsStudent(id:any): Observable<any> {
    return super.getMany<any>('GetMyGroups?status='+id);
  }

  LeaveGroup(id: any): Observable<any> {
    return super.delete("id=" + id, 'LeaveGroup');
  }
  Accept(id: any): Observable<any> {
    return super.update('AcceptInvitation?key=' + id, {});
  }
  UpdateGroup(data: any): any {
    return super.update('UpdateGroup', data);
  }

  AddMember(data: any): any {
    return super.update('AddMember', data);
  }

  ViewMembers(data: any): any {
    return super.getMany('GetMembersByGroupKey?key=' + data);
  }

  GetOutMember(data: any): any {
    return super.update('GetOutMember', data);
  }

  
  GetMembersByAssignedTaskIdKey(data: any): any {
    return super.getMany('GetMembersByAssignedTaskIdKey?key='+data);
  }
}