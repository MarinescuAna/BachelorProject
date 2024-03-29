
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
    declarations: [],
    imports: [
        CommonModule
    ]
})

export class DisplayGradesModule {
    assignedTaskId:string
    fullname:string;
    groupName:string;
    assignmentTitle:string;
    gradeChecklist:string;
    gradeTeacher:string;
    gradePeerEvaluation:string;
    comment:string;
    studentID: string;
    average:string;
}