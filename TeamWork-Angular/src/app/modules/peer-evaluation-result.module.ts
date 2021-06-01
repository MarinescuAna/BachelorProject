
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
    declarations: [],
    imports: [
        CommonModule
    ]
})

export class PeerEvaluationResultModule {
    id:string;
    evaluatingStudentEmail:string;
    evaluatingStudentFullname:string;
    error:string;
}