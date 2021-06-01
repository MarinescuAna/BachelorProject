import { Component, Inject, OnInit, Optional } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import {DisplayPeerEvaluationModule} from 'src/app/modules/display-peer-evaluation.module';
import { PeerEvaluationService } from 'src/app/services/peer-evaluation.service';

@Component({
  selector: 'app-display-peer-evaluation-dialog',
  templateUrl: './display-peer-evaluation-dialog.component.html',
  styleUrls: ['./display-peer-evaluation-dialog.component.css']
})
export class DisplayPeerEvaluationDialogComponent implements OnInit {

  result: DisplayPeerEvaluationModule;
  constructor( 
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any,
  private peerService:PeerEvaluationService){
    this.peerService.GetGrade(data.data as string).subscribe(cr => {
      this.result=cr as DisplayPeerEvaluationModule;
    });
  }

  ngOnInit(): void {
  }

}
