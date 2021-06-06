import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ViewGroupsModule } from 'src/app/modules/view-groups.module';
import { AssignmentService } from 'src/app/services/assignment.service';
import { GroupService } from 'src/app/services/group-service';
import { AssignmentsListModule } from 'src/app/modules/assigments-list.module';
import { Label } from 'ng2-charts';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { DashboardService } from 'src/app/services/dashboard.service';
import { DashboardDataModule } from 'src/app/modules/dashboard-data.module';

@Component({
  selector: 'app-dashboard-view-extention',
  templateUrl: './dashboard-view-extention.component.html',
  styleUrls: ['./dashboard-view-extention.component.css']
})
export class DashboardViewExtentionComponent {
  public pieChartOptions: ChartOptions = {
    responsive: true,
    legend: {
      position: 'right',
    },
    plugins: {
      datalabels: {
        formatter: (value, ctx) => {
          const label = ctx.chart.data.labels[ctx.dataIndex];
          return label;
        },
      },
    }
  };
  public pieChartLabels: Label[];
  public pieChartDataTasksDone: number[];
  public pieChartDataTasks: number[];
  public pieChartDataPeerEvaluationGrades: number[];
  public pieChartDataChecklistEvaluationGrades: number[];
  public pieChartType: ChartType = 'pie';
  public barChartOptions: ChartOptions = {
    responsive: true,
    scales: { xAxes: [{}], yAxes: [{}] },
    plugins: {
      datalabels: {
        anchor: 'end',
        align: 'end',
      }
    }
  };  
  public barChartData: ChartDataSets[];
  public barChartType: ChartType = 'bar';
  form = new FormGroup({
    group: new FormControl('', [Validators.required]),
    assignment: new FormControl('', [Validators.required])
  });
  assignments: AssignmentsListModule[];
  groups: ViewGroupsModule[];
  panelOpenStateGrades = false;
  @Input() list: any;
  data: DashboardDataModule[];
  load = false;

  constructor(
    private groupService: GroupService,
    private assignmentService: AssignmentService,
    private dashboardService: DashboardService) {
  }

  onExpandGrades() {
    this.panelOpenStateGrades = true;
    if (this.groups == null || this.groups.length == 0) {
      this.groupService.GetListGroups(this.list.key).subscribe(cr => {
        this.groups = cr as ViewGroupsModule[];
      });
    }

    if (this.assignments == null || this.assignments.length == 0) {
      this.assignmentService.GetAssignments(this.list.key).subscribe(cr => {
        this.assignments = cr as AssignmentsListModule[];
      });
    }

  }

  onSearch() {
    this.dashboardService.GetReport(this.form.value.assignment + "*" + this.form.value.group).subscribe(cr => {
      this.data = cr as DashboardDataModule[];
      this.setLabels();
      this.setGradesAndTasks();
      this.load = true;
    });


  }

  private setLabels() {
    let labes = [];
    this.data.forEach(element => {
      labes.push(element.fullname + " (" + element.emailStudent + ")");
    });
    this.pieChartLabels = labes as Label[];
  }

  private setGradesAndTasks() {
    let tasksDone = [];
    let tasks = [];
    let peerGrades = [];
    let chkGrades = [];
    this.data.forEach(element => {
      tasksDone.push(element.tasksDone);
      tasks.push(element.tasks);
      peerGrades.push(element.peerEvaluationGrade);
      chkGrades.push(element.checklistEvaluationGrade);
    });
    this.pieChartDataTasksDone = tasksDone as number[];
    this.pieChartDataTasks = tasks as number[];
    this.barChartData= [{ data: chkGrades as number[], label: 'Checklist Evaluation' },
                      { data: peerGrades as number[], label: 'Peer Evaluation' }
                    ] as ChartDataSets[];
  }
}
