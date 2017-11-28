import { Http, ConnectionBackend, RequestOptions, RequestOptionsArgs, Response, Headers, Request } from "@angular/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import "rxjs/Rx";
import { NotificationService } from "./services/notification.service";
@Injectable()

export class HttpInterceptor extends Http
{
    public constructor(backend: ConnectionBackend, defaultOptions: RequestOptions, private notifyService: NotificationService)
    {
        super(backend, defaultOptions);

    }

    get(url: string, options?: RequestOptionsArgs): Observable<Response>
    {
        return super.get(url, options)
            .catch((error) => {
                this.notify(error);
                return Observable.throw(error);
            });       
    }
    post(url: string, body: any, options?: RequestOptionsArgs): Observable<Response>
    {
        return super.post(url, body, options)
            .catch((error) =>{
                this.notify(error);
                return Observable.throw(error);
            });
    }
    delete(url: string, options?: RequestOptionsArgs): Observable<Response>
    {
        return super.delete(url, options)
            .catch((response) =>
        {
            this.notify(response);
            return Observable.throw(response);
        });
    }

    private notify(serverResp: Response)
    {            
        let respJson = serverResp.json();
        let message: string = respJson.value;
        let statusCode: number = respJson.statusCode;
        this.notifyService.setNotification(message, statusCode);
    }
}