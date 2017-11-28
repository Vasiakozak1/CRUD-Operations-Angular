import { Injectable } from "@angular/core";
import { Http, Headers, Response, RequestOptions } from "@angular/http";
import "rxjs/add/operator/toPromise";
import { ProjectViewModel } from "../models/projectViewModel";
import { UsersProjectsViewModel } from "../models/usersProjectsViewModel";
import { HttpInterceptor } from "../http.interceptor";
@Injectable()
export class ProjectsService
{
    public constructor(private http: Http)
    {}
    public GetAllProjects(): Promise<ProjectViewModel[]>
    {
        let requestUrl: string ="api/Projects/Get";

        return this.http.get(requestUrl)
            .toPromise()
            .then(response => response.json() as ProjectViewModel[]);
    }

    public GetById(projId: number): Promise<ProjectViewModel>
    {
        let requestUrl: string ="api/Projects/Get/" + projId;

        return this.http.get(requestUrl)
            .toPromise()
            .then(response => response.json() as ProjectViewModel);
    }

    public GetProjectsByIds(projectsIds: Array<number>): Promise<ProjectViewModel[]>
    {
        let queryUrl: string = "api/Projects/getbyids";

        let header = new Headers({'Content-Type':'application/json'});
        let option = new RequestOptions({headers: header});

        return this.http.post(queryUrl, projectsIds, option)
            .toPromise()
            .then(response => response.json() as ProjectViewModel[]);
    }

    public Create(project: ProjectViewModel): void
    {
        let requestUrl: string ="api/Projects/Create";
        let header = new Headers({'Content-Type':'application/json'});
        let option = new RequestOptions({headers: header});
        this.http.post(requestUrl,project,option)
            .toPromise();
    }
    public Update(project: ProjectViewModel): Promise<any>
    {
        let requestUrl: string ="api/Projects/Update";
        let header = new Headers({'Content-Type':'application/json'});
        let option = new RequestOptions({headers: header});
        return this.http.post(requestUrl, project, option)
            .toPromise();
    }
    public Delete(id: number)
    {
        let requestUrl: string ="api/Projects/Delete/" + id;  
        this.http.delete(requestUrl)
            .toPromise();      
    }

    public AttachUser(projectId: number, userId: number)
    {
        let requestUrl: string ="api/Projects/AttachUser";
        let body = {
            projId: projectId,
            userId: userId
        }
        
        let header = new Headers({'Content-Type':'application/json'});
        let option = new RequestOptions({headers: header});
        this.http.post(requestUrl,body,option)
            .toPromise();
    }
    public DetachUser(projectId: number, userId: number)
    {
        let requestUrl: string ="api/Projects/DetachUser";
        let body = {
            projId: projectId,
            userId: userId
        }
        let header = new Headers({'Content-Type':'application/json'});
        let option = new RequestOptions({headers: header});
        this.http.post(requestUrl,body,option)
        .toPromise();
    }
}