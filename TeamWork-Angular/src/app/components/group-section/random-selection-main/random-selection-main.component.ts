import { Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GroupService } from 'src/app/services/group-service';
import { RandomGroupsCreateModule } from 'src/app/modules/random-groups-create.module';
import { ENTER, SPACE } from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material/chips';
import { Router } from '@angular/router';
import * as XLSX from 'xlsx';

export class DisplayShuffleGroups {
  email: string;
  groupName: string;
}

@Component({
  selector: 'app-random-selection-main',
  templateUrl: './random-selection-main.component.html',
  styleUrls: ['./random-selection-main.component.css']
})
export class RandomSelectionMainComponent implements OnInit {
  private pattern =/^([^@\s]+)@((?:[-a-z0-9]+\.)+[a-z]{2,})$/i;
  dataSource: any;
  readonly separatorKeysCodes = [ENTER, SPACE] as const;
  members = [];
  form : FormGroup;
  data = new RandomGroupsCreateModule();
  error='';
  matrix=[];

  constructor(private groupService: GroupService,private _formBuilder: FormBuilder, private route:Router
  ) {
  }
  ngOnInit() {
    this.form = this._formBuilder.group({
      number: ['', Validators.required]
    });
  }
  redirectTo(url: string): void {
    this.route.navigateByUrl(url);
  }

  private isEmail(search:any):boolean
  {
      var serchfind:boolean;

      let regexp = new RegExp(this.pattern);

      serchfind = regexp.test(search);

      return serchfind
  }


  uploadEmails(evt: any) {
    this.error='';
    const target : DataTransfer = <DataTransfer>(evt.target);
    const reader: FileReader = new FileReader();
    
    if (target.files.length !== 1) {
      this.error = 'Cannot use multiple files';
    }

    reader.onload = (e: any) => {
      const bstr: string = e.target.result;
      const wb: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' });
      const wsname : string = wb.SheetNames[0];
      const ws: XLSX.WorkSheet = wb.Sheets[wsname];

      this.matrix = (XLSX.utils.sheet_to_json(ws, { header: 1 })) as string[];

      for(let index=0; index<this.matrix.length;index++){
        if(this.isEmail(this.matrix[index]) && !this.members.includes(this.matrix[index][0])){
          this.members.push(this.matrix[index][0] as string);
        }
      }
    };

    reader.readAsBinaryString(target.files[0]);

  }

  add(event: MatChipInputEvent): void {
    this.error='';
    const value = (event.value + ' ').split(' ').forEach(element => {
      if (element) {
        if (this.members.includes(element)) {
          this.error=element + " has already been written!";
        } else {
          this.members.push(element);
        }
      }
    });

    event.input.value = '';
  }

  remove(member: string): void {
    const index = this.members.indexOf(member);

    if (index >= 0) {
      this.members.splice(index, 1);
    }
  }

  private initialiseWithEmpty(): void {
    let groupName = [];
    for (let index = 0; index < this.members.length; index++) {
      groupName.push("");
    }
    this.data.groupNames = groupName as string[];
  }

  onSubmitStep0() {
    this.error='';
    let no: number = parseInt(this.form.value.number);

    if (this.members.length < no || no<0) {
      this.error="You do not have enough people to create the groups!";
    } else {

      this.data.emails = this.members as string[];
      this.data.numberMax = this.form.value.number;
      this.initialiseWithEmpty();

        this.groupService.CreateGroupsRandom(this.data).subscribe(cr => {

          let result = cr as RandomGroupsCreateModule;

          if (result.error != "") {
            this.error=result.error;
          }
          else {

            this.data = cr as RandomGroupsCreateModule;
            let dataShuffle = [];

            for (let index = 0; index < this.data.emails.length; index++) {
              let insertData = new DisplayShuffleGroups();
              insertData.email = this.data.emails[index];
              insertData.groupName = this.data.groupNames[index];
              dataShuffle.push(insertData);
            }
            this.dataSource = dataShuffle as DisplayShuffleGroups[];
           }
        });
      
    }

  }

  onSentData(){
    this.error='';
    this.groupService.SentInvitationsRandom(this.data).subscribe(cr=>{
      this.data = cr as RandomGroupsCreateModule;
      if(this.data.error!=''){
        this.error=this.data.error;
      }
    });
  }
}
