import { Injectable, Injector } from '@angular/core';
import { DataService } from '../services/data.service';

@Injectable({
  providedIn: 'root'
})
export class ChatService  extends DataService {
  constructor(injector: Injector) {
    super(injector, 'Chat');
  }

  SaveMessage(message: any): any {
    return super.post<any>('SaveMessage', message);
  }

  ViewMessages(data: any): any {
    return super.getMany('GetMessages?key=' + data);
  }

  DeleteMessage(id: any): any {
    return super.delete("key=" + id, 'DeleteMessage');
  }

  UpdateMessage(data: any): any {
    return super.update('UpdateMessage', data);
  }

}
