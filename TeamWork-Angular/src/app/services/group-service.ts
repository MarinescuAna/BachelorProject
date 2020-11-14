import { Injectable, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { GroupCreateModule } from '../modules/group-create.module';
import { GroupCreateResponseModule } from '../modules/group-create-response.module';
import { DataService } from '../services/data.service';
import {JoinGroupModule} from 'src/app/modules/join-group.module';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class GroupService extends DataService {
  constructor(injector: Injector,private route: Router) {
    super(injector, 'Group');
  }

  CreateNewGroup(module: GroupCreateModule){
    super.post<GroupCreateResponseModule>('CreateGroupByUser', module).subscribe(cr => {
        this.alertService.showSucces('The group was created!');
        this.route.navigateByUrl('/my-groups');
      });
  }

  JoinToGroup(module: JoinGroupModule){
    super.post<any>('JoinToGroup', module).subscribe(cr=>{
      this.alertService.showSucces('Success. Welcome to the group!');
      this.route.navigateByUrl('/my-groups');
    });
  }

  GetMyGroupsStudent():Observable<any>{
    return super.getMany<any>('GetMyGroups');
  }
}