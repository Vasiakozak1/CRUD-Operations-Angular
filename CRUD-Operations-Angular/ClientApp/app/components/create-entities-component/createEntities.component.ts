import { Component, ErrorHandler, OnInit } from "@angular/core";
import { NgForm, FormGroup, FormControl, Validators } from "@angular/forms";
import { ProjectsService } from "../services/projects.service";
import { UsersService } from "../services/users.service";
import { ProjectViewModel } from "../models/projectViewModel";
import { UserViewModel } from "../models/userViewModel";
@Component({
    templateUrl: './createEntities.component.html',
    styleUrls: ['./createEntities.component.css']
})
export class CreateEntitiesComponent implements ErrorHandler, OnInit
{
    user: UserViewModel;
    project: ProjectViewModel;
    userForm: FormGroup;
    projectForm: FormGroup;
    ages: number[];
    days: number[];
    months: number[];
    years: number[];
    projectDateValid: number;
    
    public constructor(private ProjService: ProjectsService, private UserService: UsersService)
    {}

    handleError(error: any)
    {
        alert('occured error');
    }

    ngOnInit()
    {
        this.projectDateValid = 0;
        this.initDates();
        this.user = UserViewModel.GetUser("", "", 0);
        this.project = ProjectViewModel.GetProject("", "", 0, 0, 0, 0, 0, 0);
        this.userForm = new FormGroup({
            firstName: new FormControl('', 
            [Validators.required, Validators.minLength(3), Validators.maxLength(15), Validators.pattern('[A-Z].*')]),
            lastName: new FormControl('',
            [Validators.required, Validators.minLength(3), Validators.maxLength(20), Validators.pattern('[A-Z].*')]),
            age: new FormControl('', Validators.required)
        });
        this.projectForm = new FormGroup({
            name: new FormControl('',
            [Validators.required, Validators.minLength(3), Validators.maxLength(25), Validators.pattern('[A-Z].*')]),
            description: new FormControl('',
            [Validators.required, Validators.minLength(5), Validators.maxLength(40), Validators.pattern('[A-Z].*')]),
            startDay: new FormControl('', Validators.required),
            startMonth: new FormControl('',Validators.required),
            startYear: new FormControl('', Validators.required),
            endDay: new FormControl('', Validators.required),
            endMonth: new FormControl('', Validators.required),
            endYear: new FormControl('', Validators.required)
        });
    }
    dateChanged(event: any)
    {
        let controls = this.projectForm.controls;
        if(controls['startDay'].value == null || controls['startMonth'].value == null || controls['startYear'].value == null
          || controls['endDay'].value == null || controls['endMonth'].value == null || controls['endYear'].value == null)
        {
            return;
        }
        if(controls['startYear'].value > controls['endYear'].value || (controls['startYear'].value == controls['endYear'].value && controls['startMonth'].value > controls['endMonth'].value)
        || (controls['startYear'].value == controls['endYear'].value && controls['startMonth'].value == controls['endMonth'].value && controls['startDay'].value > controls['endDay'].value))
        {
            this.projectDateValid = -1;
            return;
        }
        this.projectDateValid = 1;
    }

    public createProject(projForm: NgForm)
    {
        let name: string = projForm.value.name;
        let description: string = projForm.value.description;

        let startDay: number = projForm.value.startDay;
        let startMonth: number = projForm.value.startMonth;
        let startYear: number = projForm.value.startYear;

        let endDay: number = projForm.value.endDay;
        let endMonth: number = projForm.value.endMonth;
        let endYear: number = projForm.value.endYear;

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
    private initDates()
    {
        this.ages = new Array();
        let age = 17;
        do
        {
            age++;
            this.ages.push(age);          
        }
        while(age != 110);

        this.days = new Array();
        let day = 0;
        do
        {
            day++;
            this.days.push(day);
        } while(day != 30);

        this.months = new Array();
        let month = 0;
        do
        {
            month++;
            this.months.push(month);
        }
        while(month != 12);

        this.years = new Array();
        let year = 1969;
        do
        {
            year++;
            this.years.push(year);
        } while(year != 2100)
    }
}