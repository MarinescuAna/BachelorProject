import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CheckService } from 'src/app/services/check.service';
import {InsertCheckModule} from 'src/app/modules/insert-check.module';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-create-check-dialog',
  templateUrl: './create-check-dialog.component.html',
  styleUrls: ['./create-check-dialog.component.css']
})
export class CreateCheckDialogComponent implements OnInit {
  form = new FormGroup({
    description: new FormControl('',[Validators.required])
  });
  newCheck:InsertCheckModule;
  constructor(public dialogRef: MatDialogRef<CreateCheckDialogComponent>,@Optional() @Inject(MAT_DIALOG_DATA) public data: any,private checkService: CheckService) {
    this.newCheck=data.data as InsertCheckModule;
   }

  ngOnInit(): void {
  }


  onSubmit(): void{
    this.newCheck.description=this.form.value.description;
    this.checkService.InsertCheck(this.newCheck).subscribe(cr => {
      this.checkService.alertService.showSucces("The item was created!");
      this.dialogRef.close();
    });
  }
}
