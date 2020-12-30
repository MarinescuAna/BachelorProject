import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';
import { ChatService } from 'src/app/services/chat.service';
import {MessageModule} from 'src/app/modules/message.module';

@Component({
  selector: 'app-sent-message',
  templateUrl: './sent-message.component.html',
  styleUrls: ['./sent-message.component.css']
})
export class SentMessageComponent implements OnInit {

  @Input() group:ViewGroupsModule;
  formMessage = new FormGroup({
    content: new FormControl('',[Validators.required])
  });
  constructor(private chatService: ChatService, private route: Router) { }

  ngOnInit(): void {
  }

  onSubmit(): void{
    const temp=new MessageModule();
    temp.groupKey=this.group.uniqueKey;
    temp.content=this.formMessage.value.content;
    this.chatService.SaveMessage(temp).subscribe(cr => {
      window.location.reload();
    });
  }
}
