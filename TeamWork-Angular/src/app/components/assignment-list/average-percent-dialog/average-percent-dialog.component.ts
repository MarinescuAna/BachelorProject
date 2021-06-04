import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DisplayGradesModule } from 'src/app/modules/display-grades.module';
import { AverageService } from 'src/app/services/average.service';
import { AverageInsertModule } from 'src/app/modules/average-insert.module';

@Component({
  selector: 'app-average-percent-dialog',
  templateUrl: './average-percent-dialog.component.html',
  styleUrls: ['./average-percent-dialog.component.css']
})
export class AveragePercentDialogComponent implements OnInit {
  form = new FormGroup({
    grade: new FormControl('', [Validators.required]),
    peer: new FormControl('', [Validators.required]),
    chk: new FormControl('', [Validators.required]),
  });
  grades: DisplayGradesModule[];
  constructor(
    public dialogRef: MatDialogRef<AveragePercentDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public grade: any,
    private averageService: AverageService
  ) {
    this.grades = grade.grade as DisplayGradesModule[];
  }

  ngOnInit(): void {
  }

  onSubmit() {
    let grade: number= parseInt(this.form.value.grade);
    let chk: number= parseInt(this.form.value.chk);
    let peer: number= parseInt(this.form.value.peer);
    if ( grade + chk + peer != 100 ) {
      this.averageService.alertService.showWarning("The three percentages added must give 100!");
    } else {
      let data = [];
      for(let index=0;index<this.grades.length;index++){
        let average=new AverageInsertModule();
        average.assignedTaskID=this.grades[index].assignedTaskId;
        average.studentID=this.grades[index].studentID;
        average.grade=((grade/100)*parseInt(this.grades[index].gradeTeacher=='-'?'0':this.grades[index].gradeTeacher) +
                (chk/100)*parseInt(this.grades[index].gradeChecklist=='-'?'0':this.grades[index].gradeChecklist)+
                (peer/100)*parseInt(this.grades[index].gradePeerEvaluation=='-'?'0':this.grades[index].gradePeerEvaluation)
                ).toString();
        data.push(average);
      }
      this.averageService.ComputeAverage(data as AverageInsertModule[]).subscribe(cr=>{
        this.averageService.alertService.showSucces("The averages was returned!");
        this.dialogRef.close();
      });
    }
  }
}
