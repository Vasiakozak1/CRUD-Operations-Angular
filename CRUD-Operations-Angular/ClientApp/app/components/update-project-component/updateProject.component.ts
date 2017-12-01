import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute, Params } from "@angular/router";
import { NgForm } from "@angular/forms";
import { ProjectsService } from "../services/projects.service";
import { ProjectViewModel } from "../models/projectViewModel";
@Component({
    templateUrl: './updateProject.component.html'
})
export class UpdateProjectComponent implements OnInit
{
    public Project: ProjectViewModel;
    public Ready: boolean;
    public constructor(private ProjectService: ProjectsService, 
        private Router: Router, private ComponentsRoute: ActivatedRoute)
        {
            this.Ready = false;
        }

    ngOnInit()
    {
        this.ComponentsRoute.queryParams.subscribe((params: Params) =>{
            let projId: number = params['projId'];
            this.ProjectService.GetById(projId)
                .then(result => this.Project = result as ProjectViewModel)
                .then(() => this.Ready = true);
        })
    }
    updateProject(projForm: NgForm)
    {
        let name: string = projForm.value.projName;
        let description: string = projForm.value.projDescription;

        let startDay: number = projForm.value.projStartDay;
        let startMonth: number = projForm.value.projStartMonth;
        let startYear: number = projForm.value.projStartYear;

        let endDay: number = projForm.value.projEndDay;
        let endMonth: number = projForm.value.projEndMonth;
        let endYear: number = projForm.value.projEndYear;

        let proj: ProjectViewModel = ProjectViewModel
            .GetProject(name, description, startDay, 
            startMonth, startYear, endDay, endMonth, endYear);
        proj.id = this.Project.id;

        this.ProjectService.Update(proj)
            .then(() => this.Router.navigate(['/projects']));              
        
    }
}