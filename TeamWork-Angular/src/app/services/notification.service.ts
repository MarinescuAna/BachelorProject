import { Injectable, Injector } from '@angular/core';
import { DataService } from '../services/data.service';

@Injectable({
  providedIn: 'root'
})
export class NotificationService  extends DataService {
  constructor(injector: Injector) {
    super(injector, 'Notification');
  }

  public MarkAsSeen(id:any):any{
    return super.delete('notificationId='+id, 'MarkAsSeen');
  }
  public GetNotifications():any{
    return super.getMany<any>('GetNotifications');
  }
  public GetNotificationsNumber():any{
    return super.getMany<any>('GetNotificationsNumber');
  }
}