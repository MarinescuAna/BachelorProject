
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
    declarations: [],
    imports: [
        CommonModule
    ]
})

export class AssignedTasksPerGroupModule {
    assignedTaskId:string;
    groupName: string;
    assignmentTitle: string;
    groupId: string;
    assignmentDeadline: string;
    chkListDeadline: string;
    solution:string;
    grade:string;
    status:string;
    statusChecklist:string;
}