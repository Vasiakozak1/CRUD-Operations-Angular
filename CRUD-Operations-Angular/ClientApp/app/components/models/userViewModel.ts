import { UsersProjectsViewModel } from "./usersProjectsViewModel";
export class UserViewModel
{
    public id: number;
    public firstName: string;
    public lastName: string;
    public age: number;
    public projects: UsersProjectsViewModel[];

    public constructor(){}

    // public constructor(firstName: string, lastName: string, age: number)
    // {
    //     this.Firstname = firstName;
    //     this.LastName = lastName;
    //     this.Age = age;
    // }
}