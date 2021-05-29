import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-redirect-solution-dialog',
  templateUrl: './redirect-solution-dialog.component.html',
  styleUrls: ['./redirect-solution-dialog.component.css']
})
export class RedirectSolutionDialogComponent implements OnInit {
  form:FormGroup;
  solution:string;
  constructor(@Optional() @Inject(MAT_DIALOG_DATA) public data: any) {
    this.solution=data.data as string;
    this.form = new FormGroup({
      solution: new FormControl(this.solution),
    });
   }

  ngOnInit(): void {
  }
  onSubmit(): void{
   window.open(this.solution);
  }

}
