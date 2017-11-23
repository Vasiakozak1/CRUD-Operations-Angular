import { Component, OnInit } from "@angular/core";
import { UsersService } from "../services/users.service"
import { UserViewModel } from "../models/userViewModel";

@Component({
    templateUrl: "users.component.html" 
})
export class UsersComponent implements OnInit
{
    public Users: UserViewModel[];
    public constructor(private UserService: UsersService){}

    ngOnInit()
    {
        this.UserService.GetAllUsers()
            .then(result => this.Users = result as UserViewModel[]);
    }
}