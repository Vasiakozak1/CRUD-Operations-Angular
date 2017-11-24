import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Params } from "@angular/router";
import { ProjectsService } from "../services/projects.service";
import { UsersService } from "../services/users.service";
import { ProjectViewModel } from "../models/projectViewModel";
import { UserViewModel } from "../models/userViewModel";
@Component({
    templateUrl: './projectUsers.component.html'
})
export class ProjectUsersComponent implements OnInit{
    
    public Users: UserViewModel[];
    public Project: ProjectViewModel;
    public Ready: boolean;
    public constructor(private ProjectService: ProjectsService,private UserService: UsersService,
        private ComponentsRoute: ActivatedRoute)
    {
        this.Ready = false;
    }

    ngOnInit()
    {
        this.ComponentsRoute.queryParams.subscribe((param: Params) => {
            let projectId: number = param['projId'];

            if(projectId == 0)
            {
                //error
                return;
            }
            this.ProjectService.GetById(projectId)
                .then((result) => this.Project = result as ProjectViewModel)
                .then(() => {
                    let projectUsers = this.Project.users;
                    let usersIds = new Array<number>(projectUsers.length);
                    for(let i = 0;i < usersIds.length; i++)
                    {
                        usersIds[i] = projectUsers[i].userId;
                    }
                    this.UserService.GetUsersByIds(usersIds)
                        .then(result => this.Users = result as UserViewModel[])
                        .then(() => this.Ready = true);
                });

        });
    }
}