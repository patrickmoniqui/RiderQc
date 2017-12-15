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

  textValue: string;
  isInbox = true;
  Receiver = "";
  inbox = [];
  outbox = [];
  currMsg = "";
  focusedMsg: Message;
  destinataire: User = null;
  destinataireName = "";
  destinataireNameError: string;
  messageSent: string;
  nTxtAreaDisabled = false;

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
    this.userService.getUser(this.destinataireName).subscribe(
      (daUser) => {
        this.destinataire = daUser;
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
    this.userService.getUser(this.destinataireName).subscribe(
      (daUser) => {
        this.destinataire = daUser;
        
        if (this.destinataire != null && this.textValue != "") {
          let jsonBody = {
            'Receiver': this.destinataireName,
            'MessageText': this.textValue
          }
          if (this.messageService.sendMessage(jsonBody))
          {
            this.messageSent = "Successfully sent.";
          }
          else {
            this.messageSent = "Error."
          }
        }
        else
        {
          this.messageSent = "Error."
        }
      },
      (err) => {
        this.destinataireNameError = "Invalid user.";
        this.destinataire = null;
      }
    );
  }

  refresh()
  {
    this.fetchAllMessage();
  }
}
