import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PeerEvaluationResultModule } from 'src/app/modules/peer-evaluation-result.module';
import { UpdatePeerEvaluationModule } from 'src/app/modules/update-peer-evaluation.module';
import { PeerEvaluationService } from 'src/app/services/peer-evaluation.service';

@Component({
  selector: 'app-peer-evaluation-dialog',
  templateUrl: './peer-evaluation-dialog.component.html',
  styleUrls: ['./peer-evaluation-dialog.component.css']
})
export class PeerEvaluationDialogComponent implements OnInit {
  form = new FormGroup({
    grade: new FormControl('', [Validators.required]),
    comment: new FormControl('', [Validators.required]),
  });
  result: PeerEvaluationResultModule;
  constructor(
    public dialogRef: MatDialogRef<PeerEvaluationDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any,
    private peerService: PeerEvaluationService
  ) {
    this.result = data.data as PeerEvaluationResultModule;
  }

  ngOnInit(): void {
  }

  onSubmit() {
    let data = new UpdatePeerEvaluationModule();
    data.peerEvaluationId = this.result.id;
    data.grade = this.form.value.grade;
    data.comments = this.form.value.comment;

    if (parseInt(this.form.value.grade) < 0 || parseInt(this.form.value.grade) > 100) {
      this.peerService.alertService.showWarning("The grade must be between 0 and 100!");
      return;
    }
    this.peerService.AssignPeerEvaluation(data).subscribe(cr => {
      this.peerService.alertService.showSucces("The grade was returned!");
      this.dialogRef.close();
    });
  }
}
