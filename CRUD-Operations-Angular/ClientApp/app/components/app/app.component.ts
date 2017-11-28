import { Component } from '@angular/core';
import { NotificationService } from "../services/notification.service";
import { Subject } from "rxjs/Subject";
import { Subscription } from "rxjs/Subscription";
import { Notification } from "../models/notification";
@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {

    HasError: boolean;
    Notification: Notification;
    NotifSubscription: Subscription;
    public constructor(private notificator: NotificationService)
    {
        this.NotifSubscription = notificator.getNotification()
            .subscribe(notif => 
                {this.Notification = notif;
                    this.HasError = true;
                });
    }
    closeNotification()
    {
        this.notificator.closeNotification();
        this.HasError = false;
        this.NotifSubscription = this.notificator.getNotification()
        .subscribe(notif => 
            {this.Notification = notif;
                this.HasError = true;
            });
    }
}
