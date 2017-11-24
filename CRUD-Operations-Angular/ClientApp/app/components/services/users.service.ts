import { Injectable } from "@angular/core";
import { Http, Headers, RequestOptions, Response } from "@angular/http";
import "rxjs/add/operator/toPromise";

import { UserViewModel } from "../models/userViewModel";

@Injectable()
export class UsersService
{
    public constructor(private http: Http){}

    public GetAllUsers(): Promise<UserViewModel[]>
    {
        let queryUrl: string = "api/Users/Get";

        return this.http.get(queryUrl)
            .toPromise()
            .then(response => response.json() as UserViewModel[]);
    }
    public CreateUser(user: UserViewModel): void
    {
        let queryUrl: string ="api/Users/Create";
        let header = new Headers({'Content-Type':'application/json'});
        let option = new RequestOptions({headers: header});

        this.http.post(queryUrl,user, option)
            .toPromise();
    }

    public GetById(id: number): Promise<UserViewModel>
    {
        let queryUrl: string = "api/Users/Get/" + id;
        
        return this.http.get(queryUrl)
            .toPromise()
            .then(response => response.json() as UserViewModel);
    }

    public GetUsersByIds(ids: Array<number>): Promise<UserViewModel[]>
    {
        let queryUrl: string ="api/Users/getusersbyids";
        let header = new Headers({'Content-Type':'application/json'});
        let option = new RequestOptions({headers: header});    
        
        return this.http.post(queryUrl,ids, option)
            .toPromise()
            .then(response => response.json() as UserViewModel[]);
    }
    public Update(user: UserViewModel): Promise<any>
    {
        let queryUrl: string = "api/Users/Update";
        let header = new Headers({'Content-Type':'application/json'});
        let option = new RequestOptions({headers: header});    

        return this.http.post(queryUrl, user,option)
            .toPromise();
    }
    public Delete(id: number)
    {
        let queryUrl: string = "api/Users/Delete/" + id;

        this.http.delete(queryUrl)
            .toPromise();
    }
}