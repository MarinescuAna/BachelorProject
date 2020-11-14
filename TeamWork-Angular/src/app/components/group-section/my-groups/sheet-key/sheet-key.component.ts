import { Component, Inject, OnInit } from '@angular/core';
import { MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA } from '@angular/material/bottom-sheet';

@Component({
  selector: 'app-sheet-key',
  templateUrl: './sheet-key.component.html',
  styleUrls: ['./sheet-key.component.css']
})
export class SheetKeyComponent {
  constructor(@Inject(MAT_BOTTOM_SHEET_DATA) public data: {key: string}) {}


}
