import { Component, Output, EventEmitter } from "@angular/core";

import { Student } from "../student";
@Component({
    selector: 'student-form',
    templateUrl: './studentForm.component.html'
})

export class StudentFormComponent
{
    @Output() studentCreated = new EventEmitter<Student>();

    createStudent(name: string, specialization: string)
    {
        this.studentCreated.emit(new Student(name, specialization));
    }
}