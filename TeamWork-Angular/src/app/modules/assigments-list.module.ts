
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
    declarations: [],
    imports: [
        CommonModule
    ]
})

export class AssignmentsListModule {
    assignmentId:string;
    listId: string;
    title: string;
    description: string;
    deadline: string;
    checklistDeadline: string;
    groupsMax: string;
    createdDate:string;
    groupsTake:string;
    status:string;
    returnedGrade:string;
}