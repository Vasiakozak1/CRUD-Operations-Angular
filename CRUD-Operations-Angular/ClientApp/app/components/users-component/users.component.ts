import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { UsersService } from "../services/users.service"
import { UserViewModel } from "../models/userViewModel";

@Component({
    templateUrl: "users.component.html" 
})
export class UsersComponent implements OnInit
{
    public Users: UserViewModel[];
    public constructor(private UserService: UsersService, private Router: Router){}

    ngOnInit()
    {
        this.UserService.GetAllUsers()
            .then(result => this.Users = result as UserViewModel[]);
    }
    redirectToManageProjects(userId: number)
    {
        this.Router.navigate(['./manageProjects'], {queryParams: {userId: userId}});
    }
    redirectToUserProjects(userId: number)
    {
        this.Router.navigate(['./userProjects'],{queryParams: {userId: userId}});
    }
    updateUser(id: number)
    {
        this.Router.navigate(['/editUser'],{queryParams: {userId: id}});
    }
    deleteUser(id: number)
    {
        let userIndex = this.Users.findIndex(u => u.id == id);
        this.Users.splice(userIndex, 1);
        this.UserService.Delete(id);
    }
}