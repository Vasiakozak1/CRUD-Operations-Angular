import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";
import { ProjectsService } from "../services/projects.service";
import { UsersService } from "../services/users.service";
import { ProjectViewModel } from "../models/projectViewModel";
import { UserViewModel } from "../models/userViewModel";
@Component({
    templateUrl: './createEntities.component.html',
    styleUrls: ['./createEntities.component.css']
})
export class CreateEntitiesComponent
{

    public constructor(private ProjService: ProjectsService, private UserService: UsersService)
    {}

    public createProject(projForm: NgForm)
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
        this.ProjService.Create(proj);
    }
    public createUser(userForm: NgForm)
    {
        let firstName: string = userForm.value.firstName;
        let lastName: string = userForm.value.lastName;
        let age: number = userForm.value.age;
        let user: UserViewModel = new UserViewModel();
        user.age = age; user.firstName = firstName; user.lastName = lastName;
        this.UserService.CreateUser(user);
    }
}