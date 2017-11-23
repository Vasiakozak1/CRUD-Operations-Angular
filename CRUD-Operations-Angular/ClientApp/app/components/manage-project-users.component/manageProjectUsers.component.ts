import { Component, OnInit } from "@angular/core";
import { UsersService } from "../services/users.service";
import { ProjectsService } from "../services/projects.service";
import { ActivatedRoute, Params } from "@angular/router";

import { ProjectViewModel } from "../models/projectViewModel";
import { UserViewModel } from "../models/userViewModel";
import { UsersProjectsViewModel } from "../models/usersProjectsViewModel";
@Component({
    templateUrl: "./manageProjectUsers.component.html"
})
export class ManageProjectUsersComponent implements OnInit
{
    public Users: UserViewModel[];
    public TargetProject: ProjectViewModel;

    public constructor(private ProjectService: ProjectsService, private UserService: UsersService,
        private ComponentRoute: ActivatedRoute){}

    ngOnInit()
    {
        let projectId: number = 0;
        this.ComponentRoute.queryParams.subscribe((qParams: Params) => 
        {
            projectId = qParams['projId'];
            if(projectId == 0)
            {
                //error
                return;
            }
            this.ProjectService.GetById(projectId)
                .then(result => this.TargetProject = result);
            this.UserService.GetAllUsers()
                .then(result => this.Users = result);
        }); 
    }
    addUserToProj(id: number)
    {
        for(let i = 0; i < this.TargetProject.users.length; i++)
        {
            if(id == this.TargetProject.users[i].userId)
                return;
        }
        let userProject: UsersProjectsViewModel = new UsersProjectsViewModel();
        userProject.projectId = this.TargetProject.id;
        userProject.userId = id;
        this.TargetProject.users.push(userProject);
        this.ProjectService.AttachUser(this.TargetProject.id, id);
    }
    removeUserFromProj(id: number)
    {
        for(let i = 0; i < this.TargetProject.users.length; i++)
        {
            let tempUserId = this.TargetProject.users[i].userId;
            if(id == tempUserId)
            {
                this.TargetProject.users = this.TargetProject.users.splice(i, 1);
                this.ProjectService.DetachUser(this.TargetProject.id, id);
                return;
            }
        }
    }
}