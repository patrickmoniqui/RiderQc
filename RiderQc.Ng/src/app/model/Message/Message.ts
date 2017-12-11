import { User } from "../user";

export class Message {
  public Me: boolean;
  public MessageId: number;
  public MessageText: string;
  public TimeStamp: Date;
  public Read: Date;
  public Receiver: User;
  public Sender: User;
}
