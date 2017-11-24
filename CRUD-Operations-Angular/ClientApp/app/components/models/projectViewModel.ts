import { UsersProjectsViewModel } from "./usersProjectsViewModel";
export class ProjectViewModel
{
    public id: number;
    public name: string;
    public description: string;

    public startDay: number;
    public startMonth: number;
    public startYear: number;

    public endDay: number;
    public endMonth: number;
    public endYear: number;
    public users: UsersProjectsViewModel[];

    public static GetProject(name: string, description: string, startDay: number, startMonth: number,
        startYear: number, endDay: number, endMonth: number, endYear: number): ProjectViewModel
        {
            let result: ProjectViewModel = new ProjectViewModel();
            result.name = name;
            result.description = description;
            result.startDay = startDay;
            result.startMonth = startMonth;
            result.startYear = startYear;
            result.endDay = endDay;
            result.endMonth = endMonth;
            result.endYear = endYear;
            return result;
        }

    public constructor(){}
    // public constructor(name: string, description: string,
    //     startDay: number, startMonth: number, startYear: number,
    //     endDay: number, endMonth: number, endYear: number, users: UsersProjectsViewModel[])
    // {
    //     this.Name = name;
    //     this.Description = description;
    //     this.StartDay = startDay;
    //     this.StartMonth = startMonth;
    //     this.StartYear = startYear;
    //     this.EndDay = endDay;
    //     this.EndMonth = endMonth;
    //     this.EndYear = endYear;
    //     this.Users = users;
    // }

    
}