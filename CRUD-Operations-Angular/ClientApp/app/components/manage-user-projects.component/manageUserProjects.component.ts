import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Params } from "@angular/router";

import { ProjectsService } from "../services/projects.service";
import { UsersService } from "../services/users.service";
import { UserViewModel } from "../models/userViewModel";
import { UsersProjectsViewModel } from "../models/usersProjectsViewModel";
import { ProjectViewModel } from "../models/projectViewModel";

@Component({
    templateUrl: './manageUserProjects.component.html'
})
export class ManageUserProjectsComponent implements OnInit
{
    public ReadyCount: number;
    public User: UserViewModel;
    public Projects: ProjectViewModel[];
    public constructor(private ComponentsRoute: ActivatedRoute,
        private UserService: UsersService, private ProjectService: ProjectsService)
        {
            this.ReadyCount = 0;
        }

    ngOnInit()
    {
        this.ComponentsRoute.queryParams.subscribe((params: Params) => {
            let userId: number = params['userId'];
            this.UserService.GetById(userId)
                .then(result => this.User = result as UserViewModel)
                .then(() => this.ReadyCount++);
            this.ProjectService.GetAllProjects()
                .then(result => this.Projects = result as ProjectViewModel[])
                .then(() => this.ReadyCount++);

        }); 
    }
    addProjToUser(id: number)
    {
        for(let i = 0; i < this.User.projects.length; i++)
        {
            if(this.User.projects[i].projectId == id)
                return;
        }
        let projectUser: UsersProjectsViewModel = UsersProjectsViewModel.GetUserProjectModel(this.User.id, id);
        this.User.projects.push(projectUser);
        this.ProjectService.AttachUser(id, this.User.id);
    }
    removeUserFromProj(id: number)
    {
        for(let i = 0; i < this.User.projects.length; i++)
        {
            if(this.User.projects[i].projectId == id)
            {
                this.User.projects.splice(i, 1);
                this.ProjectService.DetachUser(id, this.User.id);
                return;
            }
        }
    }
}