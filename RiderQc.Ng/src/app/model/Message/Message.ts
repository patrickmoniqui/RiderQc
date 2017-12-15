import { SimpleUser } from "../simple_user"

export class Message {
  public Me: boolean;
  public MessageId: number;
  public MessageText: string;
  public TimeStamp: Date;
  public Read: Date;
  public Receiver: SimpleUser;
  public Sender: SimpleUser;
}
