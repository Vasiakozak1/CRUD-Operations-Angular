import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Params, Router } from "@angular/router";
import { NgForm } from "@angular/forms";

import { UserViewModel } from "../models/userViewModel";
import { UsersService } from "../services/users.service";

@Component({
    templateUrl: './updateUser.component.html'
})
export class UpdateUserComponent implements OnInit
{
    public User: UserViewModel;
    public Ready: boolean;
    public constructor(private UserService: UsersService, 
        private ComponentsRoute: ActivatedRoute, private Router: Router)
    {
        this.Ready = false;
    }

    ngOnInit()
    {
        this.ComponentsRoute.queryParams.subscribe((params: Params) =>
        {   
            let userId: number = params['userId'];
            this.UserService.GetById(userId)
                .then((result) => this.User = result as UserViewModel)
                .then(() =>this.Ready = true);
        })
    }
    updateUser(userForm: NgForm)
    {
        let firstName: string = userForm.value.firstName;
        let lastName: string = userForm.value.lastName;
        let age: number = userForm.value.age;
        let user: UserViewModel = new UserViewModel();
        user.firstName = firstName;
        user.lastName = lastName;
        user.age = age;
        user.id = this.User.id;
        this.UserService.Update(user)
            .then(() => this.Router.navigate(['/users']));
    }
}