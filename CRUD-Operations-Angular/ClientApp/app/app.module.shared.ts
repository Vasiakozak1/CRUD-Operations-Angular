import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule, XHRBackend, RequestOptions } from '@angular/http';
import { RouterModule } from '@angular/router';
import { ProjectsService } from "../app/components/services/projects.service";
import { UsersService } from "../app/components/services/users.service";

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { ProjectsComponent } from "./components/projects-component/projects.component";
import { CreateEntitiesComponent } from "../app/components/create-entities-component/createEntities.component";
import { UsersComponent } from "../app/components/users-component/users.component";
import { ManageProjectUsersComponent } from "../app/components/manage-project-users.component/manageProjectUsers.component";
import { ProjectUsersComponent } from "../app/components/project-users-component/projectUsers.component";
import { UpdateProjectComponent } from "../app/components/update-project-component/updateProject.component";
import { ManageUserProjectsComponent } from "../app/components/manage-user-projects.component/manageUserProjects.component";
import { UpdateUserComponent } from "../app/components/update-user-component/updateUser.component";
import { UserProjectsComponent } from "../app/components/userProjects-component/userProjects.component";
import { NotificationService } from "./components/services/notification.service";
import { HttpInterceptor } from "../app/components/http.interceptor";
import { StudentFormComponent } from "../app/components/students/student-form-component/studentForm.component";
import { StudentsListComponent } from "../app/components/students/students-list-component/studentsList.component";
import { StudentComponent } from "../app/components/students/student-component/student.component";

@NgModule({
    declarations: [
        ProjectsComponent,
        UsersComponent,
        AppComponent,
        NavMenuComponent,
        CreateEntitiesComponent,
        ManageProjectUsersComponent,
        ProjectUsersComponent,
        UserProjectsComponent,
        UpdateProjectComponent,
        ManageProjectUsersComponent,
        ManageUserProjectsComponent,
        UpdateUserComponent,
        StudentFormComponent,
        StudentComponent,
        StudentsListComponent   
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            {path: '', redirectTo: 'projects', pathMatch: 'full' },
            {path: 'projects', component: ProjectsComponent},
            {path: 'users', component: UsersComponent},
            {path: 'create',component: CreateEntitiesComponent},
            {path: 'manageUsers', component: ManageProjectUsersComponent},
            {path: 'projectUsers', component: ProjectUsersComponent},
            {path: 'editProj',component: UpdateProjectComponent},
            {path: 'manageProjects',component: ManageUserProjectsComponent},
            {path: 'editUser',component: UpdateUserComponent},
            {path: 'userProjects', component: UserProjectsComponent},
            {path: 'students', component: StudentsListComponent}
        ])
    ],
    providers:
    [
        NotificationService,
        ProjectsService,
        UsersService,      
        {
            provide: HttpInterceptor,
            useFactory:
                (backend: XHRBackend, defaultOptions: RequestOptions, notifyService: NotificationService) =>
                {
                    return new HttpInterceptor(backend,defaultOptions, notifyService);
                },
                deps: [XHRBackend, RequestOptions, NotificationService]
        }
    ]
})
export class AppModuleShared {
}
