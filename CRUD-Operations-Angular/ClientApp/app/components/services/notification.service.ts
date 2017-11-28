import { Injectable } from "@angular/core";
import { Subscription } from "rxjs/Subscription";
import { Subject } from "rxjs/Subject";
import { Observable } from "rxjs/Observable";
import { Notification } from "../models/notification";
@Injectable()
export class NotificationService
{
    private notification: Subject<Notification>;
    public constructor()
    {
        this.notification = new Subject();
    }
    public setNotification(message: string, statusCode: number)
    {
        let notification: Notification = new Notification(message, statusCode);
        this.notification.next(notification);
    }    
    public getNotification(): Observable<Notification>
    {
        return this.notification.asObservable();
    }
    public closeNotification()
    {
        this.notification = new Subject();
    }
}