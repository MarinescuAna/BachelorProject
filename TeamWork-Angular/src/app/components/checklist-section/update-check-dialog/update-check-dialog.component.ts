import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { InsertCheckModule } from 'src/app/modules/insert-check.module';
import { UpdateCheckModule } from 'src/app/modules/update-check.module';
import { CheckService } from 'src/app/services/check.service';

@Component({
  selector: 'app-update-check-dialog',
  templateUrl: './update-check-dialog.component.html',
  styleUrls: ['./update-check-dialog.component.css']
})
export class UpdateCheckDialogComponent implements OnInit {

  form:any;
  newCheck:UpdateCheckModule;
  constructor(public dialogRef: MatDialogRef<UpdateCheckDialogComponent>,@Optional() @Inject(MAT_DIALOG_DATA) public data: any,private checkService: CheckService) {
    this.newCheck=data.data as UpdateCheckModule;
    this.form = new FormGroup({
      description: new FormControl(this.newCheck.description,[Validators.required])
    });
   }

  ngOnInit(): void {
  }


  onSubmit(): void{
    this.newCheck.description=this.form.value.description;
    this.checkService.UpdateCheck(this.newCheck).subscribe(cr => {
      this.checkService.alertService.showSucces("The item was updated!");
      this.dialogRef.close();
    });
  }

}
