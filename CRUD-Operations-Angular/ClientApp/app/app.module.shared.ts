import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { ProjectsService } from "../app/components/services/projects.service";
import { UsersService } from "../app/components/services/users.service";

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { ProjectsComponent } from "./components/projects-component/projects.component";
import { CreateEntitiesComponent } from "../app/components/create-entities-component/createEntities.component";
import { UsersComponent } from "../app/components/users-component/users.component";
import { ManageProjectUsersComponent } from "../app/components/manage-project-users.component/manageProjectUsers.component";
@NgModule({
    declarations: [
        ProjectsComponent,
        UsersComponent,
        AppComponent,
        NavMenuComponent,
        CreateEntitiesComponent,
        ManageProjectUsersComponent      
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            {path: '', redirectTo: 'projects', pathMatch: 'full' },
            {path: 'projects', component: ProjectsComponent},
            {path: 'users', component: UsersComponent},
            {path: 'create',component: CreateEntitiesComponent},
            {path: 'manageUsers', component: ManageProjectUsersComponent}
            
        ])
    ],
    providers:
    [
        ProjectsService,
        UsersService
    ]
})
export class AppModuleShared {
}
