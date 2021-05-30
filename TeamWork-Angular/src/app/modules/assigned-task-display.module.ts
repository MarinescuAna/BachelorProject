
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
    declarations: [],
    imports: [
        CommonModule
    ]
})

export class AssignedTaskModule {
    assignedTaskId:string;
    assignmentID:string;
    listID: string;
    title: string;
    description: string;
    deadline: string;
    checklistDeadline: string;
    teacherGrade: string;
    solutionLink:string;
    statusTask:string;
    statusChecklist:string;
}