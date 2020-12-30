import { UpdateMessageModule } from 'src/app/modules/update-message.module';
import { Component, Inject, Input, OnInit, Optional } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ChatService } from 'src/app/services/chat.service';

@Component({
  selector: 'app-message-change',
  templateUrl: './message-change.component.html',
  styleUrls: ['./message-change.component.css']
})
export class MessageChangeComponent implements OnInit {
  
  message:any;
  formMessage : FormGroup;
  constructor(private chatService: ChatService,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any)  {

     }

  ngOnInit(): void {
    this.message=this.data;
    this.formMessage=new FormGroup({
      content: new FormControl(this.message['data'].content,[Validators.required])
    });
  }

  onSubmit(): void{
    const temp=new UpdateMessageModule();
    temp.messageKey=this.message['data'].messageKey;
    temp.content=this.formMessage.value.content;
    this.chatService.UpdateMessage(temp).subscribe(cr => {
      window.location.reload();
    });
  }

}
