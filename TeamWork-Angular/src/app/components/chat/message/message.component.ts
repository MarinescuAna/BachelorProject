import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ViewMessageModule } from 'src/app/modules/view-message.module';
import { ChatService } from 'src/app/services/chat.service';
import { MessageChangeComponent } from '../message-change/message-change.component';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit {

  @Input() message:ViewMessageModule;

  constructor(private dialog: MatDialog, private service: ChatService) { }

  ngOnInit(): void {
  }

  onUpdate(): void {
    debugger
    const diagRef = this.dialog.open(MessageChangeComponent, { data: { data: this.message } });
  }

  onDelete(){
    if (confirm("Are you sure?")) {
      var key =this.message.messageKey;
      this.service.DeleteMessage(key).subscribe(cr => {
        //tre facut ceva aici
      })
    }
  }
}
