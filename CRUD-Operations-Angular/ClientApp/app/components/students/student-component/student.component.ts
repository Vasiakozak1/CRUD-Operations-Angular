import { Component, Input } from "@angular/core";

import { Student } from "../student";
@Component({
    selector: 'student',
    template: `
    <div class="card card-block">
        <h4 class="card-title">{{student.name}}</h4>
        <h5 class="card-text">{{student.specialization}}</h5> 
    </div>
    `
})

export class StudentComponent
{
    @Input('studentProp') student: Student;
}