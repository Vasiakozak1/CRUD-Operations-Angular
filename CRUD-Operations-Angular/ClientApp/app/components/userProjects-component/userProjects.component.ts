import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Params } from "@angular/router";
import { ProjectsService } from "../services/projects.service";
import { UsersService } from "../services/users.service";
import { ProjectViewModel } from "../models/projectViewModel";
import { UserViewModel } from "../models/userViewModel";

@Component({
    templateUrl: './userProjects.component.html'
})

export class UserProjectsComponent implements OnInit
{
    public Ready: boolean;
    public User: UserViewModel;
    public Projects: ProjectViewModel[];    
    public constructor(private UserService: UsersService, 
        private ProjectService: ProjectsService, private ComponentsRoute: ActivatedRoute)
        {
            this.Ready = false;
        }

    ngOnInit()
    {
        this.ComponentsRoute.queryParams.subscribe((params: Params) =>{
            let userId: number = params['userId'];

            this.UserService.GetById(userId)
                .then(result => this.User = result as UserViewModel)
                .then(()=>{
                    let projectsIds = new Array<number>(this.User.projects.length);
                    for(let i = 0; i < projectsIds.length; i++)
                    {
                        projectsIds[i] = this.User.projects[i].projectId;
                    }
                    this.ProjectService.GetProjectsByIds(projectsIds)
                        .then(result => this.Projects = result as ProjectViewModel[])
                        .then(() => this.Ready = true);
                });
        });
    }
}