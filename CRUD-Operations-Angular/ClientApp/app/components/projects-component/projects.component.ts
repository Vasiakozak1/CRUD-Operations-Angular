import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { ProjectViewModel } from "../models/projectViewModel";
import { ProjectsService } from "../services/projects.service";

@Component({
    templateUrl: './projects.component.html'
})
export class ProjectsComponent implements OnInit
{
    public Projects: ProjectViewModel[];

    public constructor(private ProjectService: ProjectsService, private Router: Router)
    {
    }

    ngOnInit()
    {
        this.ProjectService.GetAllProjects()
            .then(result => this.Projects = result as ProjectViewModel[]);         
    }
    redirectToManageUsers(id: number)
    {
        this.Router.navigate(['/manageUsers'], {queryParams: {projId: id}});
    }
    redirectToShowUsers(id: number)
    {
        this.Router.navigate(['/projectUsers'],{queryParams: {projId: id}});
    }
    deleteProj(id: number)
    {
        let projIndex = this.Projects.findIndex(proj => proj.id == id);
        this.Projects.splice(projIndex, 1);
        this.ProjectService.Delete(id);
    }
    editProj(id: number)
    {
        this.Router.navigate(['/editProj'],{queryParams: {projId: id}});
    }
}