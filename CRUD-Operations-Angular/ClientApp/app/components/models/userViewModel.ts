import { UsersProjectsViewModel } from "./usersProjectsViewModel";
export class UserViewModel
{
    public id: number;
    public firstName: string;
    public lastName: string;
    public age: number;
    public projects: UsersProjectsViewModel[];

    public constructor(){}

    public static GetUser(firstName: string, lastName: string, age: number): UserViewModel
    {
        let user: UserViewModel = new UserViewModel();
        user.id = 0;
        user.firstName = firstName;
        user.lastName = lastName;
        user.age = age;
        return user; 
    }
    // public constructor(firstName: string, lastName: string, age: number)
    // {
    //     this.Firstname = firstName;
    //     this.LastName = lastName;
    //     this.Age = age;
    // }
}