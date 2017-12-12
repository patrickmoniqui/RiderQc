import { Component } from '@angular/core';

import { UserService } from '../../services/user.service';
import { MessagerieService } from '../../services/messagerie.service';

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
  currMsg = "";

  //// userService.getBearerAuthHeader();

  constructor(public messageService: MessagerieService, public userService: UserService) {
    this.fetchAllMessage();
  }

  fetchAllMessage() {
    this.messageService.getMessages().subscribe((messages) => {
      console.log(messages);
      this.inbox = messages;
    });
    /*this.messageService.getSendMessages().subscribe((messages) => {
      this.outbox = messages;
    });*/
  }

  openCity(cityName:any): void {
    if (cityName == 'Inbox') {
      this.isInbox = true;
    } else {
      this.isInbox = false;
    }
  }

  sendMessage() {
    this.messageService.sendMessage(this.currMsg);
  }
}
