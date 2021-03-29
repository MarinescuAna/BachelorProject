import { Component, Input, OnInit } from '@angular/core';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';
import{ViewMessageModule} from 'src/app/modules/view-message.module';
import{ChatService} from 'src/app/services/chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  @Input() group: ViewGroupsModule;
  messages: ViewMessageModule[];
  constructor(private chatService: ChatService) {

   }

  ngOnInit(): void {
    this.chatService.ViewMessages(this.group.uniqueKey).subscribe(cr =>{
      this.messages= cr as ViewMessageModule[];
    });
  }

}
