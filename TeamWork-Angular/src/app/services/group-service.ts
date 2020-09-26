import { Injectable, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { GroupCreateModule } from '../modules/group-create.module';
import { GroupCreateResponseModule } from '../modules/group-create-response.module';
import { DataService } from '../services/data.service';

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
        //localStorage.setItem('is_logged', 'true');
        //this.route.navigateByUrl('/create-group');
      });
  }
}