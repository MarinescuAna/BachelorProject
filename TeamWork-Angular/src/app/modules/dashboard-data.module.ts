
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
    declarations: [],
    imports: [
        CommonModule
    ]
})

export class DashboardDataModule {
    emailStudent:string
    fullname:string;
    groupName:string;
    tasksDone:string;
    tasks:string;
    peerEvaluationGrade:string;
    checklistEvaluationGrade:string;
    error: string;
}