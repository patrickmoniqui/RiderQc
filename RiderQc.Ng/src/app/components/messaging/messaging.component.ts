import { Component } from '@angular/core';

@Component({
  selector: 'app-messaging',
  templateUrl: './messaging.component.html',
  styleUrls: ['./messaging.component.css']
})
/** messaging component*/
export class MessagingComponent {

  isInbox = true;
  inbox = [];
  outbox = [];

  constructor() {

  }

  openCity(cityName:any): void {
    if (cityName == 'Inbox') {
      this.isInbox = true;
    } else {
      this.isInbox = false;
    }
  }
}
