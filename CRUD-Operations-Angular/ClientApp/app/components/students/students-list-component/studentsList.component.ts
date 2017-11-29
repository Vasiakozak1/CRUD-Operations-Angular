import { Component } from "@angular/core";
import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";

import { Student } from "../student";
@Component({
    template: `
    <div class="row"> 
        <div class="col-md-4"> </div>
        <div class="col-md-5">
            <student-form
                (studentCreated)="addStudents($event)">
            </student-form>
            <student *ngFor="let stud of students" [studentProp]="stud"> </student>
        </div>
    </div>
    `
})
export class StudentsListComponent {
    public students: Student[];
    public constructor() {
        this.students = [new Student('Vania', 'Sust progr')];
    }

    addStudents(student: Student) {
        this.students.unshift(student);
        this.createPromise()
            .then((result) => console.log(result),
                  (result) => console.log('error:'+result));
    }

    GetFib(iteration: number): number
    {
        if(iteration <= 0)
            return 0;
        if(iteration == 1)
            return 1;
        return this.GetFib(iteration -2) + this.GetFib(iteration -1);
    }

    createPromise(): Promise<any>
    {
        let fun: Function = () => console.log('function is working');
        fun.call(this)
        var promise = new Promise(
            (reject, resolve) =>
            {
                let fibValue: number=0;
                for(let i = 0; i <= 20; i++)
                {
                    fibValue = this.GetFib(i);
                    
                    console.log('fib[' + i + ']=' + fibValue);
                }               
                reject('end value:'+fibValue);
            }
        );
        return promise;
    }
}