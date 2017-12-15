import { Component } from '@angular/core';

import { UserService } from '../../services/user.service';
import { MessagerieService } from '../../services/messagerie.service';
import { User } from '../../model/user';
import { Message } from '../../model/Message/Message';

@Component({
  selector: 'app-messaging',
  templateUrl: './messaging.component.html',
  styleUrls: ['./messaging.component.css']
})
/** messaging component*/
export class MessagingComponent {

  isInbox = true;
  Receiver = "";
  inbox = [];
  outbox = [];
  currMsg = "";
  focusedMsg: Message;
  destinataire: User = null;
  destinataireName = "";
  nTxtAreaDisabled = false;

  //// userService.getBearerAuthHeader();

  constructor(public messageService: MessagerieService, public userService: UserService) {
    this.fetchAllMessage();
  }

  fetchAllMessage() {
    this.messageService.getInbox().subscribe((messages) => {
      this.inbox = messages;
    });
    this.messageService.getOutbox().subscribe((messages) => {
      this.outbox = messages;
    });
  }

  openCity(cityName:any): void {
    if (cityName == 'Inbox') {
      this.isInbox = true;
    } else {
      this.isInbox = false;
    }
    if (this.nTxtAreaDisabled) {
      this.destinataireName = "";
      this.currMsg = "";
    }
  }

  checkDestinateur() {
    console.log("INNNN");
    this.userService.getUser(this.destinataireName).subscribe(
      (daUser) => {
        this.destinataire = daUser;
        console.log("gotem " + JSON.stringify(daUser));
      },
      (err) => {
        this.destinataire = null;
      }
    );

  }

  reply() {
    this.nTxtAreaDisabled = false;
    this.currMsg = "\n\n--" + this.focusedMsg.TimeStamp + "--" + this.destinataireName + "-- \n\n" + this.currMsg;
  }

  getStyle() {
    if (this.destinataire != null) {
      return {
        'color': '#00ffaa'
      }
    } else {
      return {
        'color': '#FF0000'
      }
    }
  }

  msgSelect(msg) {
    console.log(JSON.stringify(msg));
    this.focusedMsg = msg;
    this.currMsg = msg.MessageText;
    this.destinataireName = ""; //*******
    this.nTxtAreaDisabled = true;
  }

  newMsg() {
    this.destinataireName = "";
    this.currMsg = "";
    this.nTxtAreaDisabled = false;
    this.isInbox = true;
  }

  setBg(nRead) {
    if (nRead) {
      return {
        'background-color': '#222222'
      }
    } else {
      return {
        'background-color': '#777777'
      }
    }
  }

  sendMessage() {
    console.log(JSON.stringify(this.destinataire) + " " + this.currMsg);
    if (this.destinataire != null && this.currMsg != "") {
      let jsonBody = {
        'Receiver': this.destinataireName,/*this.destinataire.UserID.toString(),*/
        'MessageText': this.currMsg
      }
      this.messageService.sendMessage(jsonBody);
    }

  }
}
